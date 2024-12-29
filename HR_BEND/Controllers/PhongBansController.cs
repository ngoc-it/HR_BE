using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR_BEND.Models.Data;
using Microsoft.AspNetCore.Authorization;

namespace HR_BEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PhongBansController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PhongBansController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PhongBans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhongBan>>> GetPhongBans()
        {
            return await _context.PhongBans.ToListAsync();
        }

        // GET: api/PhongBans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhongBan>> GetPhongBan(int id)
        {
            var phongBan = await _context.PhongBans.FindAsync(id);

            if (phongBan == null)
            {
                return NotFound();
            }

            return phongBan;
        }

        // PUT: api/PhongBans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutPhongBan(int id, PhongBan phongBan)
        {
            if (id != phongBan.Id)
            {
                return BadRequest();
            }

            _context.Entry(phongBan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhongBanExists(id))
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

*/
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhongBan(int id, PhongBan phongBan)
        {
            // Kiểm tra nếu ID trong URL khớp với ID trong đối tượng phongBan
            if (id != phongBan.Id)
            {
                return BadRequest(); // Nếu không khớp, trả về lỗi Bad Request
            }

            // Tìm phòng ban cần cập nhật trong cơ sở dữ liệu
            var existingPhongBan = await _context.PhongBans.FindAsync(id);
            if (existingPhongBan == null)
            {
                return NotFound(); // Nếu không tìm thấy, trả về Not Found
            }

            // Cập nhật các trường cần thiết
            existingPhongBan.PhongBanID = phongBan.PhongBanID; // Cập nhật phongBanID
            existingPhongBan.TenPhongBan = phongBan.TenPhongBan; // Cập nhật tenPhongBan

            // Lưu thay đổi
            await _context.SaveChangesAsync();

            return NoContent(); // Trả về 204 No Content nếu thành công
        }

        // POST: api/PhongBans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhongBan>> PostPhongBan(PhongBan phongBan)
        {
            _context.PhongBans.Add(phongBan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhongBan", new { id = phongBan.Id }, phongBan);
        }

        // DELETE: api/PhongBans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhongBan(int id)
        {
            var phongBan = await _context.PhongBans.FindAsync(id);
            if (phongBan == null)
            {
                return NotFound();
            }

            _context.PhongBans.Remove(phongBan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhongBanExists(int id)
        {
            return _context.PhongBans.Any(e => e.Id == id);
        }
    }
}
