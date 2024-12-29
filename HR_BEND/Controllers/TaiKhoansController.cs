using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HR_BEND.Models.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HR_BEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoansController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<TaiKhoan> _passwordHasher;
        public TaiKhoansController(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<TaiKhoan>();
        }
        // GET: api/TaiKhoan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaiKhoan>>> GetTaiKhoans()
        {
            return await _context.TaiKhoans.Include(t => t.ChucVu).Include(t => t.NhanVien).ToListAsync();
        }

        // GET: api/TaiKhoan/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaiKhoan>> GetTaiKhoan(int id)
        {
            var taiKhoan = await _context.TaiKhoans.Include(t => t.ChucVu).Include(t => t.NhanVien).FirstOrDefaultAsync(t => t.Id == id);

            if (taiKhoan == null)
            {
                return NotFound();
            }

            return taiKhoan;
        }

        // POST: api/TaiKhoan
        [HttpPost]
        public async Task<ActionResult<TaiKhoan>> PostTaiKhoan(TaiKhoanCRUDModel model)
        {

        // Lấy thông tin nhân viên từ cơ sở dữ liệu
        var nhanVien = await _context.NhanViens.FindAsync(model.NhanVienId);

            if (nhanVien == null)
            {
                return NotFound();  // Nếu không tìm thấy nhân viên
            }

            // Lấy chức vụ của nhân viên từ bảng nhân viên
            var chucVu = await _context.ChucVus.FindAsync(nhanVien.ChucVuId);
            var taiKhoan = new TaiKhoan
            {
                TenDangNhap = model.TenDangNhap,
                /*MatKhau = model.MatKhau,*/
                MatKhau = _passwordHasher.HashPassword(null, model.MatKhau),
                NhanVienId = model.NhanVienId,
                /*                ChucVuId = model.ChucVuId,*/
                ChucVuId = chucVu?.Id ?? 0,
                TrangThai = model.TrangThai,
            };

            _context.TaiKhoans.Add(taiKhoan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaiKhoan", new { id = taiKhoan.Id }, taiKhoan);
        }

        // PUT: api/TaiKhoan/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaiKhoan(int id, TaiKhoanCRUDModel model)
        {
            /*var taiKhoan = await _context.TaiKhoans.FindAsync(id);*/
            var taiKhoan = await _context.TaiKhoans.Include(t => t.NhanVien).FirstOrDefaultAsync(t => t.Id == id);

            if (taiKhoan == null)
            {
                return NotFound();
            }
            var nhanVien = await _context.NhanViens.FindAsync(model.NhanVienId);
            if (nhanVien == null)
            {
                return NotFound();  // Nếu không tìm thấy nhân viên
            }
            var chucVu = await _context.ChucVus.FindAsync(nhanVien.ChucVuId);

            taiKhoan.TenDangNhap = model.TenDangNhap;
            /*taiKhoan.MatKhau = model.MatKhau;*/
            if (!string.IsNullOrEmpty(model.MatKhau))
            {
                taiKhoan.MatKhau = _passwordHasher.HashPassword(null, model.MatKhau);
            }
            taiKhoan.NhanVienId = model.NhanVienId;
            /* taiKhoan.ChucVuId = model.ChucVuId;*/
            taiKhoan.ChucVuId = chucVu?.Id ?? 0;
            taiKhoan.TrangThai = model.TrangThai;

            _context.Entry(taiKhoan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaiKhoanExists(id))
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

        // DELETE: api/TaiKhoan/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaiKhoan(int id)
        {
            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            _context.TaiKhoans.Remove(taiKhoan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaiKhoanExists(int id)
        {
            return _context.TaiKhoans.Any(e => e.Id == id);
        }

       

        // GET: api/TaiKhoans/check-nhanvien/{nhanVienId}
        [HttpGet("check-nhanvien/{nhanVienId}")]
        public async Task<IActionResult> CheckNhanVienHasTaiKhoan(int nhanVienId)
        {
            // Sử dụng asynchronous để tối ưu hiệu suất khi truy cập database
            var hasTaiKhoan = await _context.TaiKhoans.AnyAsync(tk => tk.NhanVienId == nhanVienId);

            return Ok(new
            {
                NhanVienId = nhanVienId,
                HasTaiKhoan = hasTaiKhoan
            });
        }

       
       /* [Authorize]
        [HttpGet("getCurrentUserInfo")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            // Lấy giá trị cookie user_cookie
            var userNhanVienID = Request.Cookies["user_cookie"];

            if (string.IsNullOrEmpty(userNhanVienID))
            {
                return Unauthorized("User not authenticated");
            }

            // In ra giá trị của cookie để kiểm tra
            Console.WriteLine($"User Cookie Value: {userNhanVienID}");

            // Truy vấn cơ sở dữ liệu để lấy tài khoản theo Id (chuyển Id sang string để so sánh)
            var taiKhoan = await _context.TaiKhoans
                .Include(tk => tk.ChucVu)  // Nếu cần lấy Chức Vụ của tài khoản
                .FirstOrDefaultAsync(tk => tk.Id.ToString() == userNhanVienID);  // Chuyển Id thành string

            if (taiKhoan == null)
            {
                return NotFound("User not found");
            }

            return Ok(new
            {
                HoTen = taiKhoan.TenDangNhap,  // Hoặc các trường khác từ tài khoản
                ChucVu = taiKhoan.ChucVu?.TenChucVu  // Trả về tên chức vụ nếu có
            });
        }*/



    }


}

