/*using HR_BEND.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR_BEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInCheckOutController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CheckInCheckOutController(AppDbContext context)
        {
            _context = context;
        }
        public class CheckInRequest
        {
            public int NhanVienID { get; set; }
            public string? ViTriCheckIn { get; set; }
        }

        public class CheckOutRequest
        {
            public int NhanVienID { get; set; }
            public string? ViTriCheckOut { get; set; }
        }

        // API Check-in
        [HttpPost("CheckIn")]
        public async Task<ActionResult> CheckIn([FromBody] CheckInRequest request)
        {
            // Kiểm tra sự tồn tại của nhân viên
            var nhanVien = await _context.NhanViens.FirstOrDefaultAsync(nv => nv.Id == request.NhanVienID);
            if (nhanVien == null)
            {
                return BadRequest(new { message = "Nhân viên không tồn tại!" });
            }

            var today = DateTime.Now.Date;
            var existingChamCong = await _context.ChamCongs
                .FirstOrDefaultAsync(c => c.NhanVienID == request.NhanVienID && c.NgayChamCong == today);

            if (existingChamCong != null && existingChamCong.CheckInTime != null)
            {
                return BadRequest(new { message = "Nhân viên đã check-in trong ngày hôm nay!" });
            }

            var newChamCong = existingChamCong ?? new ChamCong
            {
                NhanVienID = request.NhanVienID,
                NgayChamCong = today,
            };

            newChamCong.CheckInTime = DateTime.Now;
            newChamCong.ViTriCheckIn = request.ViTriCheckIn; // Có thể là null

            if (existingChamCong == null)
            {
                _context.ChamCongs.Add(newChamCong);
            }
            else
            {
                _context.Entry(existingChamCong).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Check-in thành công!", checkInTime = newChamCong.CheckInTime });
        }

        // API Check-out
        [HttpPost("CheckOut")]
        public async Task<ActionResult> CheckOut([FromBody] CheckOutRequest request)
        {
            // Kiểm tra sự tồn tại của nhân viên
            var nhanVien = await _context.NhanViens.FirstOrDefaultAsync(nv => nv.Id == request.NhanVienID);
            if (nhanVien == null)
            {
                return BadRequest(new { message = "Nhân viên không tồn tại!" });
            }

            var today = DateTime.Now.Date;
            var existingChamCong = await _context.ChamCongs
                .FirstOrDefaultAsync(c => c.NhanVienID == request.NhanVienID && c.NgayChamCong == today);

            if (existingChamCong == null || existingChamCong.CheckInTime == null)
            {
                return BadRequest(new { message = "Nhân viên chưa check-in hoặc không có bản ghi chấm công trong ngày!" });
            }

            if (existingChamCong.CheckOutTime != null)
            {
                return BadRequest(new { message = "Nhân viên đã check-out trong ngày hôm nay!" });
            }

            existingChamCong.CheckOutTime = DateTime.Now;
            existingChamCong.ViTriCheckOut = request.ViTriCheckOut; // Có thể là null

            _context.Entry(existingChamCong).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Check-out thành công!", checkOutTime = existingChamCong.CheckOutTime });
        }

    }
}
*/
using HR_BEND.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR_BEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInCheckOutController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CheckInCheckOutController(AppDbContext context)
        {
            _context = context;
        }

        public class CheckInRequest
        {
            public int NhanVienID { get; set; }
            public string? ViTriCheckIn { get; set; }
        }

        public class CheckOutRequest
        {
            public int NhanVienID { get; set; }
            public string? ViTriCheckOut { get; set; }
        }

        // API Check-in
        [HttpPost("CheckIn")]
        public async Task<ActionResult> CheckIn([FromBody] CheckInRequest request)
        {
            // Kiểm tra sự tồn tại của nhân viên
            var nhanVien = await _context.NhanViens.FirstOrDefaultAsync(nv => nv.Id == request.NhanVienID);
            if (nhanVien == null)
            {
                return BadRequest(new { message = "Nhân viên không tồn tại!" });
            }

            var today = DateTime.Now.Date;
            var existingChamCong = await _context.ChamCongs
                .FirstOrDefaultAsync(c => c.NhanVienID == request.NhanVienID && c.NgayChamCong == today && c.CheckOutTime == null);

            // Kiểm tra nếu chưa check-out, có thể check-in tiếp
            if (existingChamCong != null)
            {
                return BadRequest(new { message = "Nhân viên chưa check-out lần trước!" });
            }

            // Tạo bản ghi mới cho check-in
            var newChamCong = new ChamCong
            {
                NhanVienID = request.NhanVienID,
                NgayChamCong = today,
                CheckInTime = DateTime.Now,
                ViTriCheckIn = request.ViTriCheckIn
            };

            _context.ChamCongs.Add(newChamCong);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Check-in thành công!", checkInTime = newChamCong.CheckInTime });
        }

        // API Check-out
        [HttpPost("CheckOut")]
        public async Task<ActionResult> CheckOut([FromBody] CheckOutRequest request)
        {
            // Kiểm tra sự tồn tại của nhân viên
            var nhanVien = await _context.NhanViens.FirstOrDefaultAsync(nv => nv.Id == request.NhanVienID);
            if (nhanVien == null)
            {
                return BadRequest(new { message = "Nhân viên không tồn tại!" });
            }

            var today = DateTime.Now.Date;
            var existingChamCong = await _context.ChamCongs
                .FirstOrDefaultAsync(c => c.NhanVienID == request.NhanVienID && c.NgayChamCong == today && c.CheckInTime != null && c.CheckOutTime == null);

            // Kiểm tra nếu chưa check-in hoặc đã check-out
            if (existingChamCong == null)
            {
                return BadRequest(new { message = "Nhân viên chưa check-in hoặc đã check-out trong ngày!" });
            }

            // Cập nhật thời gian check-out
            existingChamCong.CheckOutTime = DateTime.Now;
            existingChamCong.ViTriCheckOut = request.ViTriCheckOut;

            _context.Entry(existingChamCong).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Check-out thành công!", checkOutTime = existingChamCong.CheckOutTime });
        }
    }
}


/*using HR_BEND.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace HR_BEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInCheckOutController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CheckInCheckOutController(AppDbContext context)
        {
            _context = context;
        }

        public class CheckInRequest
        {
            public string? ViTriCheckIn { get; set; }
        }

        public class CheckOutRequest
        {
            public string? ViTriCheckOut { get; set; }
        }

        // Lấy NhanVienID từ thông tin người dùng đã đăng nhập
        private int GetNhanVienIDFromClaims()
        {
            var userClaim = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userClaim != null && int.TryParse(userClaim.Value, out var nhanVienId))
            {
                return nhanVienId;
            }
            throw new UnauthorizedAccessException("Không tìm thấy NhanVienID trong thông tin người dùng.");
        }

        // API Check-in
        [HttpPost("CheckIn")]
        public async Task<ActionResult> CheckIn([FromBody] CheckInRequest request)
        {
            try
            {
                // Lấy NhanVienID từ claims của người dùng đã đăng nhập
                int nhanVienID = GetNhanVienIDFromClaims();

                // Kiểm tra sự tồn tại của nhân viên
                var nhanVien = await _context.NhanViens.FirstOrDefaultAsync(nv => nv.Id == nhanVienID);
                if (nhanVien == null)
                {
                    return BadRequest(new { message = "Nhân viên không tồn tại!" });
                }

                var today = DateTime.Now.Date;
                var existingChamCong = await _context.ChamCongs
                    .FirstOrDefaultAsync(c => c.NhanVienID == nhanVienID && c.NgayChamCong == today && c.CheckOutTime == null);

                // Kiểm tra nếu chưa check-out, có thể check-in tiếp
                if (existingChamCong != null)
                {
                    return BadRequest(new { message = "Nhân viên chưa check-out lần trước!" });
                }

                // Tạo bản ghi mới cho check-in
                var newChamCong = new ChamCong
                {
                    NhanVienID = nhanVienID,
                    NgayChamCong = today,
                    CheckInTime = DateTime.Now,
                    ViTriCheckIn = request.ViTriCheckIn
                };

                _context.ChamCongs.Add(newChamCong);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Check-in thành công!", checkInTime = newChamCong.CheckInTime });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        // API Check-out
        [HttpPost("CheckOut")]
        public async Task<ActionResult> CheckOut([FromBody] CheckOutRequest request)
        {
            try
            {
                // Lấy NhanVienID từ claims của người dùng đã đăng nhập
                int nhanVienID = GetNhanVienIDFromClaims();

                // Kiểm tra sự tồn tại của nhân viên
                var nhanVien = await _context.NhanViens.FirstOrDefaultAsync(nv => nv.Id == nhanVienID);
                if (nhanVien == null)
                {
                    return BadRequest(new { message = "Nhân viên không tồn tại!" });
                }

                var today = DateTime.Now.Date;
                var existingChamCong = await _context.ChamCongs
                    .FirstOrDefaultAsync(c => c.NhanVienID == nhanVienID && c.NgayChamCong == today && c.CheckInTime != null && c.CheckOutTime == null);

                // Kiểm tra nếu chưa check-in hoặc đã check-out
                if (existingChamCong == null)
                {
                    return BadRequest(new { message = "Nhân viên chưa check-in hoặc đã check-out trong ngày!" });
                }

                // Cập nhật thời gian check-out
                existingChamCong.CheckOutTime = DateTime.Now;
                existingChamCong.ViTriCheckOut = request.ViTriCheckOut;

                _context.Entry(existingChamCong).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Check-out thành công!", checkOutTime = existingChamCong.CheckOutTime });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
*/
