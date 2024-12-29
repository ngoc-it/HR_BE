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
    public class CongViecsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CongViecsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CongViecs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CongViec>>> GetCongViec()
        {
            
            return await _context.CongViec.ToListAsync();
        }

        // GET: api/CongViecs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CongViec>> GetCongViec(int id)
        {
            var congViec = await _context.CongViec.FindAsync(id);

            if (congViec == null)
            {
                return NotFound();
            }

            return congViec;
        }

        // PUT: api/CongViecs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCongViec(int id, CongViec congViec)
        {
            if (id != congViec.Id)
            {
                return BadRequest();
            }

            _context.Entry(congViec).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CongViecExists(id))
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

        // POST: api/CongViecs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CongViec>> PostCongViec(CongViecCRUDModel congViec)
        {
            if (congViec.NguoiTaoId == 0)
            {
                return BadRequest(new { Message = "NguoiTaoId là bắt buộc" });
            }
            _context.CongViec.Add(new CongViec()
                {
                CongViecId = congViec.CongViecId,
                TenCongViec = congViec.TenCongViec,
                MoTa = congViec.MoTa,
                TrangThai = congViec.TrangThai,
                NgayTao = congViec.NgayTao,
                NgayHoanThanh = congViec.NgayHoanThanh,
                NgayHoanThanhDuKien = congViec.NgayHoanThanhDuKien,
                NguoiTaoId = congViec.NguoiTaoId,
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

            return CreatedAtAction("GetCongViec", new { id = congViec.CongViecId }, congViec);
        }

        // DELETE: api/CongViecs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCongViec(int id)
        {
            var congViec = await _context.CongViec.FindAsync(id);
            if (congViec == null)
            {
                return NotFound();
            }

            _context.CongViec.Remove(congViec);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CongViecExists(int id)
        {
            return _context.CongViec.Any(e => e.Id == id);
        }


        [HttpGet("NhanVien/{nhanVienId}")]
        public async Task<ActionResult<IEnumerable<CongViec>>> GetCongViecByNhanVienId(int nhanVienId)
        {
            // Lọc các bản ghi chấm công của nhân viên dựa vào NhanVienID
            var congViecs = await _context.CongViec
                .Where(c => c.NguoiTaoId == nhanVienId)
                .ToListAsync();

            if (congViecs == null || congViecs.Count == 0)
            {
                return NotFound(new { message = "Không có dữ liệu công việc cho nhân viên này." });
            }

            return Ok(congViecs);
        }
    }
}
