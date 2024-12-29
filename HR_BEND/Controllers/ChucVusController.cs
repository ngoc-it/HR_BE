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
    public class ChucVusController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChucVusController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ChucVus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChucVu>>> GetChucVus()
        {
            return await _context.ChucVus.ToListAsync();
        }

        // GET: api/ChucVus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChucVu>> GetChucVu(int id)
        {
            var chucVu = await _context.ChucVus.FindAsync(id);

            if (chucVu == null)
            {
                return NotFound();
            }

            return chucVu;
        }

        // PUT: api/ChucVus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChucVu(int id, ChucVu chucVu)
        {
            if (id != chucVu.Id)
            {
                return BadRequest();
            }

            _context.Entry(chucVu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChucVuExists(id))
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

        // POST: api/ChucVus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChucVu>> PostChucVu(ChucVu chucVu)
        {
            _context.ChucVus.Add(chucVu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChucVu", new { id = chucVu.Id }, chucVu);
        }

        // DELETE: api/ChucVus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChucVu(int id)
        {
            var chucVu = await _context.ChucVus.FindAsync(id);
            if (chucVu == null)
            {
                return NotFound();
            }

            _context.ChucVus.Remove(chucVu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChucVuExists(int id)
        {
            return _context.ChucVus.Any(e => e.Id == id);
        }
    }
}
