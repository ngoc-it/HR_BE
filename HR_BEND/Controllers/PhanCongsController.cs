using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR_BEND.Models.Data;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace HR_BEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanCongsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PhanCongsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PhanCongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhanCong>>> GetPhanCongs()
        {

            return await _context.PhanCongs.ToListAsync();
        }

        // GET: api/PhanCongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhanCong>> GetPhanCong(int id)
        {
            var phanCong = await _context.PhanCongs.FindAsync(id);

            if (phanCong == null)
            {
                return NotFound();
            }

            return phanCong;
        }

        // PUT: api/PhanCongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhanCong(int id, PhanCong phanCong)
        {
            if (id != phanCong.Id)
            {
                return BadRequest();
            }

            _context.Entry(phanCong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhanCongExists(id))
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

        // POST: api/PhanCongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhanCong>> PostPhanCong(PhanCongCRUDModel phanCong)
        {
            if (phanCong.CongViecId == 0 || phanCong.NguoiDuocPhanCongId ==0 || phanCong.NguoiPhanCongId ==0)
            {
                return BadRequest(new { Message = "CongViecId là bắt buộc! " });

            }
            _context.PhanCongs.Add(new PhanCong()
            {
                PhanCongId = phanCong.PhanCongId,
                CongViecId = phanCong.CongViecId,
                TenCongViecPhanCong = phanCong.TenCongViecPhanCong,
                NguoiDuocPhanCongId = phanCong.NguoiDuocPhanCongId,
                NguoiPhanCongId = phanCong.NguoiPhanCongId,
                NgayBatDau = phanCong.NgayBatDau,
                NgayHoanThanh = phanCong.NgayHoanThanh,
                TrangThai = phanCong.TrangThai,
                GhiChu = phanCong.GhiChu
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


            return CreatedAtAction("GetPhanCong", new { id = phanCong.PhanCongId }, phanCong);
        }

        // DELETE: api/PhanCongs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhanCong(int id)
        {
            var phanCong = await _context.PhanCongs.FindAsync(id);
            if (phanCong == null)
            {
                return NotFound();
            }

            _context.PhanCongs.Remove(phanCong);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhanCongExists(int id)
        {
            return _context.PhanCongs.Any(e => e.Id == id);
        }
        // GET: api/PhanCongs/nguoiDuocPhanCong/5
        [HttpGet("nguoiDuocPhanCong/{id}")]
        public async Task<ActionResult<IEnumerable<PhanCong>>> GetPhanCongsByNguoiDuocPhanCongId(int id)
        {
            var phanCongs = await _context.PhanCongs
                .Where(pc => pc.NguoiDuocPhanCongId == id) // Lọc theo người được phân công
                .Include(pc => pc.CongViec) // Giả sử có relation với Công việc
                .Include(pc => pc.NguoiDuocPhanCong) // Giả sử có relation với người được phân công
                .Include(pc => pc.NguoiPhanCong) // Giả sử có relation với người phân công
                .ToListAsync();

            if (phanCongs == null || !phanCongs.Any())
            {
                return NotFound();
            }

            return phanCongs;
        }

        // GET: api/PhanCongs/nguoiPhanCong/5
        [HttpGet("nguoiPhanCong/{id}")]
        public async Task<ActionResult<IEnumerable<PhanCong>>> GetPhanCongsByNguoiPhanCongId(int id)
        {
            var phanCongs = await _context.PhanCongs
                .Where(pc => pc.NguoiPhanCongId == id) // Lọc theo người phân công
                .Include(pc => pc.CongViec) // Giả sử có relation với Công việc
                .Include(pc => pc.NguoiDuocPhanCong) // Giả sử có relation với người được phân công
                .Include(pc => pc.NguoiPhanCong) // Giả sử có relation với người phân công
                .ToListAsync();

            if (phanCongs == null || !phanCongs.Any())
            {
                return NotFound();
            }

            return phanCongs;
        }
        


    }
}

