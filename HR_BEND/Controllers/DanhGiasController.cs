using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR_BEND.Models.Data;

namespace HR_BEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhGiasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DanhGiasController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DanhGia>>> GetDanhGias()
        {

            return await _context.DanhGias.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DanhGia>> GetDanhGias(int id)
        {
            var danhGia = await _context.DanhGias.FindAsync(id);

            if (danhGia == null)
            {
                return NotFound();
            }

            return danhGia;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDanhGia(int id, DanhGia danhGia)
        {
            if (danhGia.DiemDanhGia < 0.0 || danhGia.DiemDanhGia > 10.0)
            {
                return BadRequest("Điểm đánh giá phải nằm trong khoảng từ 0.0 đến 10.0");
            }
            if (id != danhGia.Id)
            {
                return BadRequest();
            }

            _context.Entry(danhGia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DanhGiaExists(id))
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
        [HttpPost]
        public async Task<ActionResult<PhanCong>> PostDanhGia(DanhGiaCRUDModel danhGia)
        {
            if (danhGia.DanhGiaId == 0 || danhGia.NguoiDanhGiaId == 0)
            {
                return BadRequest(new { Message = "NguoiDanhGiaId là bắt buộc! " });

            }
            if (danhGia.DiemDanhGia < 0.0 || danhGia.DiemDanhGia > 10.0)
            {
                return BadRequest("Điểm đánh giá phải nằm trong khoảng từ 0.0 đến 10.0");
            }
            _context.DanhGias.Add(new DanhGia()
            {
                 DanhGiaId = danhGia.DanhGiaId,
        PhanCongId = danhGia.PhanCongId,
        NgayDanhGia =danhGia.NgayDanhGia,
        DiemDanhGia = danhGia.DiemDanhGia,
       GhiChu = danhGia.GhiChu,
       NguoiDanhGiaId = danhGia.NguoiDanhGiaId

    }); try
            {
                await _context.SaveChangesAsync();
                Console.WriteLine("Dữ liệu đã được lưu vào cơ sở dữ liệu.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu dữ liệu: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi hệ thống");
            }


            return CreatedAtAction("GetDanhGias", new { id = danhGia.PhanCongId }, danhGia);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDanhGia(int id)
        {
            var danhGia = await _context.DanhGias.FindAsync(id);
            if (danhGia == null)
            {
                return NotFound();
            }

            _context.DanhGias.Remove(danhGia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool DanhGiaExists(int id)
        {
            return _context.DanhGias.Any(e => e.Id == id);
        }
        [HttpGet("NhanVien/{nhanVienId}")]
        public async Task<ActionResult<IEnumerable<DanhGia>>> GetDanhGiaByNhanVienId(int nhanVienId)
        {
            // Lọc các bản ghi chấm công của nhân viên dựa vào NhanVienID
            var danhGias = await _context.DanhGias
                .Where(c => c.Id == nhanVienId)
                .ToListAsync();

            if (danhGias == null || danhGias.Count == 0)
            {
                return NotFound(new { message = "Không có dữ liệu đánh giá cho nhân viên này." });
            }

            return Ok(danhGias);
        }
        [HttpGet("PhanCong/{phanCongId}")]
        public async Task<ActionResult<IEnumerable<DanhGia>>> GetDanhGiaByPhanCongId(int phanCongId)
        {
            // Lọc các bản ghi đánh giá của công việc dựa vào PhanCongId
            var danhGias = await _context.DanhGias
                .Where(d => d.PhanCongId == phanCongId)
                .ToListAsync();

            if (danhGias == null || danhGias.Count == 0)
            {
                return NotFound(new { message = "Không có dữ liệu đánh giá cho công việc này." });
            }

            return Ok(danhGias);
        }

    }
}
