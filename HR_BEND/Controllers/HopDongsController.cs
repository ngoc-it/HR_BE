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
    public class HopDongsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public HopDongsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HopDong>>> HopDongs()
        {

            return await _context.HopDongs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HopDong>> GetHopDongs(int id)
        {
            var hopDong = await _context.HopDongs.FindAsync(id);

            if (hopDong == null)
            {
                return NotFound();
            }

            return hopDong;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHopDong(int id, HopDong hopDong)
        {
            if (id != hopDong.Id)
            {
                return BadRequest();
            }

            _context.Entry(hopDong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HopDongExists(id))
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
        public async Task<ActionResult<HopDong>> PostHopDong(HopDongCRUDModel hopDong)
        {
            if (hopDong.HopDongId == 0 || hopDong.NhanVienId == 0)
            {
                return BadRequest(new { Message = "HopDongId là bắt buộc! " });

            }
            _context.HopDongs.Add(new HopDong()
            {
                HopDongId = hopDong.HopDongId,
                TenHopDong = hopDong.TenHopDong,
                NhanVienId = hopDong.NhanVienId,
                NgayBatDau = hopDong.NgayBatDau,
                NgayKetThuc = hopDong.NgayKetThuc,
                LuongCoBan = hopDong.LuongCoBan,
                TrangThai  = hopDong.TrangThai,
                GhiChu = hopDong.GhiChu,
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


            return CreatedAtAction("GetHopDongs", new { id = hopDong.HopDongId }, hopDong);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHopDong(int id)
        {
            var hopDong = await _context.HopDongs.FindAsync(id);
            if (hopDong == null)
            {
                return NotFound();
            }

            _context.HopDongs.Remove(hopDong);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool HopDongExists(int id)
        {
            return _context.HopDongs.Any(e => e.Id == id);
        }
        // GET: api/TinhLuongs/NhanVien/5
        [HttpGet("NhanVien/{nhanVienId}")]
        public async Task<ActionResult<IEnumerable<HopDong>>> GetHopDongByNhanVienId(int nhanVienId)
        {
            // Lọc các bản ghi chấm công của nhân viên dựa vào NhanVienID
            var hopDongs = await _context.HopDongs
                .Where(c => c.NhanVienId == nhanVienId)
                .ToListAsync();

            if (hopDongs == null || hopDongs.Count == 0)
            {
                return NotFound(new { message = "Không có dữ liệu hợp đồng cho nhân viên này." });
            }

            return Ok(hopDongs);
        }
    }
}

