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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;

namespace HR_BEND.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginLogoutController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<LoginLogoutController> _logger;
        private readonly PasswordHasher<TaiKhoan> _passwordHasher;
        public LoginLogoutController(
         AppDbContext context,
         ILogger<LoginLogoutController> logger)
        {
            _context = context;
            _logger = logger;
            _passwordHasher = new PasswordHasher<TaiKhoan>();

        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taiKhoan = await _context.TaiKhoans
                .Include(t => t.ChucVu)
                .Include(t => t.NhanVien)
                .FirstOrDefaultAsync(t => t.TenDangNhap == model.TenDangNhap);

            if (taiKhoan == null)
            {
                return Unauthorized("Tên đăng nhập không đúng.");
            }

            var result = _passwordHasher.VerifyHashedPassword(taiKhoan, taiKhoan.MatKhau, model.MatKhau);
            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Mật khẩu không đúng.");
            }

            // Tạo danh sách claims cho token
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, taiKhoan.TenDangNhap),
        new Claim(ClaimTypes.Role, taiKhoan.ChucVu.TenChucVu),
        new Claim(ClaimTypes.NameIdentifier, taiKhoan.NhanVienId.ToString())
    };

            // Tạo khóa bảo mật (Secret Key)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9")); // Thay bằng khóa bí mật thực tế
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Thiết lập thời gian sống của token
            var token = new JwtSecurityToken(
                issuer: "http://localhost/5215", // Thay bằng giá trị Issuer thực tế
                audience: "http://localhost:5215", // Thay bằng giá trị Audience thực tế
                claims: claims,
                expires: DateTime.Now.AddDays(7), // Token có hiệu lực trong 7 ngày
                signingCredentials: creds
            );

            // Trả về token
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        // POST: api/TaiKhoans/logout
        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            return Ok("Đăng xuất thành công. Vui lòng xóa token từ phía client.");
        }

        [HttpGet("getCurrentUserInfo")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            // Lấy token từ header Authorization
            var token = Request.Headers["Authorization"].ToString();

            // Kiểm tra xem token có hợp lệ không
            if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
            {
                return Unauthorized("Token không hợp lệ.");
            }

            token = token.Substring("Bearer ".Length).Trim(); // Trích xuất token từ header

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                // Nếu không giải mã được token
                if (jwtToken == null)
                {
                    return Unauthorized("Token không thể giải mã.");
                }

                // Lấy userId từ claims trong token
                var userIdFromToken = jwtToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                // Kiểm tra nếu không có userId trong token
                if (string.IsNullOrEmpty(userIdFromToken))
                {
                    return Unauthorized("Không tìm thấy userId trong token.");
                }

                // Lấy thông tin người dùng từ cơ sở dữ liệu
                var nhanVien = await _context.NhanViens
                    .Include(tk => tk.ChucVu)
                    .FirstOrDefaultAsync(tk => tk.Id.ToString() == userIdFromToken);

                // Kiểm tra xem người dùng có tồn tại không
                if (nhanVien == null)
                {
                    return NotFound("Không tìm thấy người dùng.");
                }

                // Trả về thông tin người dùng
                return Ok(new
                {
                    HoTen = nhanVien.HoTen,
                    ChucVu = nhanVien.ChucVu?.TenChucVu
                });
            }
            catch (Exception ex)
            {
                // Nếu có lỗi xảy ra trong quá trình xác thực token
                return Unauthorized($"Xác thực token không thành công: {ex.Message}");
            }
        }



        public class JwtTokenService
        {
            private readonly IConfiguration _configuration;

            public JwtTokenService(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public string GenerateJwtToken(string userIdFromToken)
            {
                var claims = new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, userIdFromToken), // Thêm userId vào claim
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }


    }
}
