using HR_BEND.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace HR_BEND.Models.Service
{
    public interface IChamCongService
    {
        Task<double> GetTongSoNgayCong(int nhanVienId);
    }

    public class ChamCongService : IChamCongService
    {
        private readonly AppDbContext _context;

        public ChamCongService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<double> GetTongSoNgayCong(int nhanVienId)
        {
            var chamCongs = await _context.ChamCongs
                .Where(c => c.NhanVienID == nhanVienId)
                .ToListAsync();

            if (chamCongs == null || chamCongs.Count == 0)
            {
                throw new Exception("Không có dữ liệu chấm công cho nhân viên.");
            }

            double tongSoGioCong = 0;
            foreach (var chamCong in chamCongs)
            {
                if (chamCong.CheckInTime != null && chamCong.CheckOutTime != null)
                {
                    var checkInTime = chamCong.CheckInTime.Value;
                    var checkOutTime = chamCong.CheckOutTime.Value;

                    var soGioCong = (checkOutTime - checkInTime).TotalHours;
                    tongSoGioCong += soGioCong;
                }
            }

            return tongSoGioCong / 8;  // Chia cho 8 để có số ngày công thực tế
        }
    }

}
