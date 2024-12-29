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
    public class ChamCongsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChamCongsController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: api/ChamCongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChamCong>>> GetChamCongs()
        {
            return await _context.ChamCongs.ToListAsync();
        }

        // GET: api/ChamCongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChamCong>> GetChamCong(int id)
        {
            var chamCong = await _context.ChamCongs.FindAsync(id);

            if (chamCong == null)
            {
                return NotFound();
            }

            return chamCong;
        }

        // PUT: api/ChamCongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChamCong(int id, ChamCong chamCong)
        {
            if (id != chamCong.Id)
            {
                return BadRequest();
            }

            _context.Entry(chamCong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChamCongExists(id))
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

        // POST: api/ChamCongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChamCong>> PostChamCong(ChamCongCRUDModel chamCong)
        {
            /*_context.ChamCongs.Add(chamCong);*/

            /*await _context.SaveChangesAsync();

            return CreatedAtAction("GetChamCong", new { id = chamCong.Id }, chamCong);*/
            if (chamCong.NhanVienID == 0)
            {
                return BadRequest(new {Message = "NhanVienID là bắt buộc"});
            }
            _context.ChamCongs.Add(new ChamCong()
            {
                Id = chamCong.Id,
                 NhanVienID = chamCong.NhanVienID,
       NgayChamCong = chamCong.NgayChamCong,
        CheckInTime =chamCong.CheckInTime,
        CheckOutTime =chamCong.CheckOutTime,
        ViTriCheckIn = chamCong.ViTriCheckIn,
        ViTriCheckOut = chamCong.ViTriCheckOut,
        
    });
            try
            {
                await _context.SaveChangesAsync();
                Console.WriteLine("Dữ liệu đã được lưu vào cơ sở dữ liệu.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu dữ liệu: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi hệ thống");
            }
            return CreatedAtAction("GetChamCong", new { id = chamCong.Id }, chamCong);
        }

        // DELETE: api/ChamCongs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChamCong(int id)
        {
            var chamCong = await _context.ChamCongs.FindAsync(id);
            if (chamCong == null)
            {
                return NotFound();
            }

            _context.ChamCongs.Remove(chamCong);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChamCongExists(int id)
        {
            return _context.ChamCongs.Any(e => e.Id == id);
        }

        [HttpGet("TongSoNgayCong/{nhanVienId}")]
        public async Task<ActionResult<double>> GetTongSoNgayCong(int nhanVienId)
        {
            // Lấy tất cả bản ghi chấm công của nhân viên theo ID
            var chamCongs = await _context.ChamCongs
                .Where(c => c.NhanVienID == nhanVienId)
                .ToListAsync();

            if (chamCongs == null || chamCongs.Count == 0)
            {
                return NotFound(new { message = "Không có dữ liệu chấm công cho nhân viên." });
            }

            double tongSoGioCong = 0;

            // Duyệt qua tất cả các bản ghi chấm công và tính tổng số giờ công
            foreach (var chamCong in chamCongs)
            {
                if (chamCong.CheckInTime != null && chamCong.CheckOutTime != null)
                {
                    // Lấy thời gian check-in và check-out
                    var checkInTime = chamCong.CheckInTime.Value;
                    var checkOutTime = chamCong.CheckOutTime.Value;

                    // Tính tổng số giờ công
                    var soGioCong = (checkOutTime - checkInTime).TotalHours;

                    // Cộng dồn số giờ công vào tổng số giờ
                    tongSoGioCong += soGioCong;
                }
            }

            // Chia tổng số giờ công cho 8 để tính ra số ngày công
            double soNgayCongThucTe = tongSoGioCong / 8;

            return Ok(soNgayCongThucTe);  // Trả về số ngày công
        }
        // GET: api/ChamCongs/NhanVien/5
        [HttpGet("NhanVien/{nhanVienId}")]
        public async Task<ActionResult<IEnumerable<ChamCong>>> GetChamCongByNhanVienId(int nhanVienId)
        {
            // Lọc các bản ghi chấm công của nhân viên dựa vào NhanVienID
            var chamCongs = await _context.ChamCongs
                .Where(c => c.NhanVienID == nhanVienId)
                .ToListAsync();

            if (chamCongs == null || chamCongs.Count == 0)
            {
                return NotFound(new { message = "Không có dữ liệu chấm công cho nhân viên này." });
            }

            return Ok(chamCongs);
        }

    }

}
