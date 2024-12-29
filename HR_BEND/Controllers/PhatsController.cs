using HR_BEND.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HR_BEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhatsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PhatsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Phats
        [HttpGet]
        public async Task<ActionResult> GetPhats()
        {
            var phats = await _context.Phats.ToListAsync();
            return Ok(phats);
        }

        // GET: api/Phats/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPhat(int id)
        {
            var phat = await _context.Phats.FindAsync(id);

            if (phat == null)
            {
                return NotFound();
            }

            return Ok(phat);
        }

        // POST: api/Phats
        [HttpPost]
        public async Task<ActionResult> PostPhat(Phat phat)
        {
            if (phat == null)
            {
                return BadRequest("Phat is null.");
            }

            _context.Phats.Add(phat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhat", new { id = phat.Id }, phat);
        }

        // PUT: api/Phats/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPhat(int id, Phat phat)
        {
            if (id != phat.Id)
            {
                return BadRequest("Phat ID mismatch.");
            }

            _context.Entry(phat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhatExists(id))
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

        // DELETE: api/Phats/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePhat(int id)
        {
            var phat = await _context.Phats.FindAsync(id);
            if (phat == null)
            {
                return NotFound();
            }

            _context.Phats.Remove(phat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhatExists(int id)
        {
            return _context.Phats.Any(e => e.Id == id);
        }
    }
}
