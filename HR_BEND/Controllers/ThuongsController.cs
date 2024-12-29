using HR_BEND.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HR_BEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuongsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ThuongsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Thuongs
        [HttpGet]
        public async Task<ActionResult> GetThuongs()
        {
            var thuongs = await _context.Thuongs.ToListAsync();
            return Ok(thuongs);
        }

        // GET: api/Thuongs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetThuong(int id)
        {
            var thuong = await _context.Thuongs.FindAsync(id);

            if (thuong == null)
            {
                return NotFound();
            }

            return Ok(thuong);
        }

        // POST: api/Thuongs
        [HttpPost]
        public async Task<ActionResult> PostThuong(Thuong thuong)
        {
            if (thuong == null)
            {
                return BadRequest("Thuong is null.");
            }

            _context.Thuongs.Add(thuong);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetThuong", new { id = thuong.Id }, thuong);
        }

        // PUT: api/Thuongs/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> PutThuong(int id, Thuong thuong)
        {
            if (id != thuong.Id)
            {
                return BadRequest("Thuong ID mismatch.");
            }

            _context.Entry(thuong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThuongExists(id))
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

        // DELETE: api/Thuongs/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteThuong(int id)
        {
            var thuong = await _context.Thuongs.FindAsync(id);
            if (thuong == null)
            {
                return NotFound();
            }

            _context.Thuongs.Remove(thuong);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ThuongExists(int id)
        {
            return _context.Thuongs.Any(e => e.Id == id);
        }
    }
}
