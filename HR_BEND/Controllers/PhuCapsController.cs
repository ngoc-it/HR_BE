using HR_BEND.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HR_BEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhuCapsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PhuCapsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PhuCaps
        [HttpGet]
        public async Task<ActionResult> GetPhuCaps()
        {
            var phuCaps = await _context.PhuCaps
                .Include(p => p.ChucVu)  // Load ChucVu data with PhuCap
                .ToListAsync();
            return Ok(phuCaps);
        }

        // GET: api/PhuCaps/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPhuCap(int id)
        {
            var phuCap = await _context.PhuCaps
                .Include(p => p.ChucVu)  // Load ChucVu data with PhuCap
                .FirstOrDefaultAsync(p => p.PhuCapID == id);

            if (phuCap == null)
            {
                return NotFound();
            }

            return Ok(phuCap);
        }

        // POST: api/PhuCaps
        [HttpPost]
        public async Task<ActionResult> PostPhuCap(PhuCap phuCap)
        {
            if (phuCap == null)
            {
                return BadRequest("PhuCap is null.");
            }

            _context.PhuCaps.Add(phuCap);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhuCap", new { id = phuCap.PhuCapID }, phuCap);
        }

        // PUT: api/PhuCaps/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPhuCap(int id, PhuCap phuCap)
        {
            if (id != phuCap.PhuCapID)
            {
                return BadRequest("PhuCap ID mismatch.");
            }

            _context.Entry(phuCap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhuCapExists(id))
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

        // DELETE: api/PhuCaps/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePhuCap(int id)
        {
            var phuCap = await _context.PhuCaps.FindAsync(id);
            if (phuCap == null)
            {
                return NotFound();
            }

            _context.PhuCaps.Remove(phuCap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhuCapExists(int id)
        {
            return _context.PhuCaps.Any(e => e.PhuCapID == id);
        }
    }
}
