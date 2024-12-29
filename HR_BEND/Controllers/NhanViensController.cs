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
    public class NhanViensController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NhanViensController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/NhanViens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NhanVien>>> GetNhanViens(int? phongBanId)
        {
            /*return await _context.NhanViens.ToListAsync();*/
            /*            return await _context.NhanViens
                    .Include(nv => nv.PhongBan)
                    .Include(nv => nv.ChucVu)
                    .ToListAsync();*/
            IQueryable<NhanVien> query = _context.NhanViens
                   .Include(nv => nv.PhongBan)
                   .Include(nv => nv.ChucVu);

            // Nếu có tham số phongBanId, lọc theo phòng ban
            if (phongBanId.HasValue)
            {
                query = query.Where(nv => nv.PhongBanId == phongBanId.Value);
            }
            return await query.ToListAsync();

        }

        // GET: api/NhanViens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NhanVien>> GetNhanVien(int id)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);

            if (nhanVien == null)
            {
                return NotFound();
            }

            return nhanVien;
        }

        // PUT: api/NhanViens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNhanVien(int id, NhanVien nhanVien)
        {
            if (id != nhanVien.Id)
            {
                return BadRequest();
            }

            _context.Entry(nhanVien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhanVienExists(id))
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

        // POST: api/NhanViens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NhanVien>> PostNhanVien(NhanVienCRUDModel nhanVien)
        {
            Console.WriteLine($"PhongBanId: {nhanVien.PhongBanId}, ChucVuId: {nhanVien.ChucVuId}");

            if (nhanVien.PhongBanId == 0 || nhanVien.ChucVuId == 0)
            {
                return BadRequest(new { Message = "PhongBanId và ChucVuId là bắt buộc" });
            }


            _context.NhanViens.Add(new NhanVien()
            {
                NhanVienID = nhanVien.NhanVienID,
                HoTen = nhanVien.HoTen,
                NgaySinh = nhanVien.NgaySinh,
                DiaChi = nhanVien.DiaChi,
                SDT = nhanVien.SDT,
                Anh = nhanVien.Anh,
                Email = nhanVien.Email,
                PhongBanId = nhanVien.PhongBanId,
                ChucVuId = nhanVien.ChucVuId,
                NgayVaoLam = nhanVien.NgayVaoLam,
                TrangThaiID = nhanVien.TrangThaiID,
                GioiTinh = nhanVien.GioiTinh
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

            return CreatedAtAction("GetNhanVien", new { id = nhanVien.NhanVienID }, nhanVien);
            //_context.NhanViens.Add(nhanVien);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetNhanVien", new { id = nhanVien.Id }, nhanVien);
        }

        // DELETE: api/NhanViens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNhanVien(int id)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            _context.NhanViens.Remove(nhanVien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NhanVienExists(int id)
        {
            return _context.NhanViens.Any(e => e.Id == id);
        }


        [HttpPost("assets")]
        public async Task<IActionResult> UploadImage(IFormFile file, Guid id)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Ensure the Uploads directory exists
            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Set the file name using GUID and the original file name
            string filePath = Path.Combine(uploadPath, $"{id}_{file.FileName}");

            // Save the file to the Uploads directory
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                // Log the error if needed
                return StatusCode(StatusCodes.Status500InternalServerError, $"File upload failed: {ex.Message}");
            }

            // Return the file URL pointing to the Uploads directory
            string fileUrl = $"{Request.Scheme}://{Request.Host}/assets/{id}_{file.FileName}";
            return Ok(new { fileUrl });
        }
        [HttpGet("NhanVien/{nhanVienId}")]
        public async Task<ActionResult<IEnumerable<NhanVien>>> GetNhanVienByNhanVienId(int nhanVienId)
        {
            // Lọc các bản ghi chấm công của nhân viên dựa vào NhanVienID
            var nhanViens = await _context.NhanViens
                .Where(c => c.Id == nhanVienId)
                .ToListAsync();

            if (nhanViens == null || nhanViens.Count == 0)
            {
                return NotFound(new { message = "Không có dữ liệu nhân viên cho nhân viên này." });
            }

            return Ok(nhanViens);
        }
        [HttpGet("CheckNhanVienID/{nhanVienID}")]
        public IActionResult CheckNhanVienID(string nhanVienID)
        {
            var exists = _context.NhanViens.Any(nv => nv.NhanVienID == nhanVienID);
            return Ok(new { exists });
        }

    }
}
