using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HR_BEND.Models.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HR_BEND.Models.Service;

namespace HR_BEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinhLuongsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IChamCongService _chamCongService;

        public TinhLuongsController(AppDbContext context, IChamCongService chamCongService)
        {
            _context = context;
            _chamCongService = chamCongService; // Tiêm dịch vụ vào
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tinhLuongs = await _context.TinhLuongs
                .Include(tl => tl.NhanVien)
                .Include(tl => tl.HopDong)
                .ToListAsync();
            return Ok(tinhLuongs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tinhLuong = await _context.TinhLuongs
                .Include(tl => tl.NhanVien)
                .Include(tl => tl.HopDong)
                .FirstOrDefaultAsync(tl => tl.Id == id);

            if (tinhLuong == null)
                return NotFound();

            return Ok(tinhLuong);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TinhLuongDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Kiểm tra sự tồn tại của nhân viên và hợp đồng
            var nhanVien = await _context.NhanViens.FindAsync(model.NhanVienId);
            if (nhanVien == null)
                return NotFound("Nhân viên không tồn tại.");

            var hopDong = await _context.HopDongs.FindAsync(model.HopDongId);
            if (hopDong == null)
                return NotFound("Hợp đồng không tồn tại.");

            // Lấy số ngày công thực tế từ dịch vụ
            double soNgayCongThucTe = await _chamCongService.GetTongSoNgayCong(model.NhanVienId);

            // Tính số ngày công và lương
            var soNgayCong = soNgayCongThucTe + (model.SoNgayNghiCoPhep ?? 0);
            double luongThucNhan = await CalculateSalary(
                model.NhanVienId,
                hopDong.LuongCoBan,
                soNgayCong,
                model.ThuongIds,
                model.PhatIds,
                model.SoNgayNghiCoPhep,
                model.SoNgayNghiKhongPhep,
                soNgayCongThucTe);

            var tinhLuong = new TinhLuong
            {
                TenTinhLuong = model.TenTinhLuong,
                ThangNam = model.ThangNam,
                SoNgayCong = soNgayCong,
                SoNgayCongThucTe = soNgayCongThucTe,
                HopDongId = model.HopDongId,
                NhanVienId = model.NhanVienId,
                LuongThucNhan = (decimal?)luongThucNhan,
                ThuongIds = model.ThuongIds,
                PhatIds = model.PhatIds,
                SoNgayNghiCoPhep = model.SoNgayNghiCoPhep,
                SoNgayNghiKhongPhep = model.SoNgayNghiKhongPhep
            };

            _context.TinhLuongs.Add(tinhLuong);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = tinhLuong.Id }, tinhLuong);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TinhLuong tinhLuong)
        {
            if (id != tinhLuong.Id)
            {
                return BadRequest();
            }

            _context.Entry(tinhLuong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TinhLuongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool TinhLuongExists(int id)
        {
            return _context.TinhLuongs.Any(e => e.Id == id);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tinhLuong = await _context.TinhLuongs.FindAsync(id);
            if (tinhLuong == null)
                return NotFound();

            _context.TinhLuongs.Remove(tinhLuong);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<(double SoNgayCongThucTe, double SoNgayCong)> CalculateWorkingDays(int nhanVienId, DateTime thangNam, double? soNgayNghiCoPhep)
        {
            // Get employee's attendance records for the given month
            var chamCongs = await _context.ChamCongs
                .Where(cc => cc.NhanVienID == nhanVienId &&
                             cc.NgayChamCong.HasValue &&
                             cc.NgayChamCong.Value.Month == thangNam.Month &&
                             cc.NgayChamCong.Value.Year == thangNam.Year)
                .ToListAsync();

            // Calculate actual working days and total working days
            /* double soNgayCongThucTe = chamCongs.Count(); // Actual working days from attendance*/
            // Get employee's actual working days for the given month from the service
            double soNgayCongThucTe = await _chamCongService.GetTongSoNgayCong(nhanVienId);

            // Calculate total working days by adding paid leave days
            double soNgayCong = soNgayCongThucTe + (soNgayNghiCoPhep ?? 0); // Add paid leave days

            // Return both actual and total working days
            return (soNgayCongThucTe, soNgayCong);
        }

        private async Task<double> CalculateSalary(int nhanVienId, double? luongCoBan, double soNgayCong, List<int> thuongIds, List<int> phatIds, double? soNgayNghiCoPhep, double? soNgayNghiKhongPhep, double? soNgayCongThucTe)
        {
            double luongNgay = luongCoBan.HasValue ? luongCoBan.Value / 22 : 0;
            double tienLuong = luongNgay * soNgayCong;

            double tienThuong = 0;
            foreach (var thuongId in thuongIds)
            {
                var thuong = await _context.Thuongs.FindAsync(thuongId);
                if (thuong != null) tienThuong += thuong.SoTienThuong;
            }

            double tienPhat = 0;
            foreach (var phatId in phatIds)
            {
                var phat = await _context.Phats.FindAsync(phatId);
                if (phat != null) tienPhat += phat.SoTien;
            }

            return tienLuong + tienThuong - tienPhat;
        }
        // GET: api/TinhLuongs/NhanVien/5
        [HttpGet("NhanVien/{nhanVienId}")]
        public async Task<ActionResult<IEnumerable<TinhLuong>>> GetTinhLuongByNhanVienId(int nhanVienId)
        {
            // Lọc các bản ghi chấm công của nhân viên dựa vào NhanVienID
            var tinhLuongs = await _context.TinhLuongs
                .Where(c => c.NhanVienId == nhanVienId)
                .ToListAsync();

            if (tinhLuongs == null || tinhLuongs.Count == 0)
            {
                return NotFound(new { message = "Không có dữ liệu tính lương cho nhân viên này." });
            }

            return Ok(tinhLuongs);
        }
        [HttpGet("CheckTinhLuongForMonth/{nhanVienId}/{thangNam}")]
        public async Task<IActionResult> CheckTinhLuongForMonth(int nhanVienId, DateTime thangNam)
        {
            var existingTinhLuong = await _context.TinhLuongs
                .FirstOrDefaultAsync(tl => tl.NhanVienId == nhanVienId &&
                                           tl.ThangNam.Month == thangNam.Month &&
                                           tl.ThangNam.Year == thangNam.Year);

            return Ok(existingTinhLuong != null);
        }



    }
}
