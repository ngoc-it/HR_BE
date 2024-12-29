using Microsoft.AspNetCore.Routing.Constraints;

namespace HR_BEND.Models.Data
{
    public class TinhLuong : BaseData
    {
        public string TenTinhLuong { get; set; }
        public DateTime ThangNam { get; set; }
        public double? SoNgayCongThucTe {  get; set; }
        public double SoNgayCong { get; set; }
        public int HopDongId { get; set; }
        public HopDong? HopDong { get; set; }
        public decimal? LuongThucNhan { get; set; }
        public int NhanVienId { get; set; }
        public NhanVien? NhanVien { get; set; }

        // Thay vì sử dụng đơn lẻ, sử dụng danh sách
        public List<int>? ThuongIds { get; set; } = new List<int>();
        public List<int>? PhatIds { get; set; } = new List<int>();
        public List<int> ChamCongIds { get; set; } = new List<int>();
        public List<ChamCong>? ChamCongs { get; set; }

        public double? SoNgayNghiCoPhep { get; set; }
        public double? SoNgayNghiKhongPhep { get; set; }

        // Có thể cần thêm PhuCapId nếu bạn dùng cho nhiều Phụ Cấp
    }


    public class TinhLuongDTO
    {
        public string TenTinhLuong { get; set; }
        public DateTime ThangNam { get; set; }
        public double? SoNgayCongThucTe {  get; set; }
        public double SoNgayCong { get; set; }
        public int HopDongId { get; set; }
        public int NhanVienId { get; set; }
        public  decimal? LuongThucNhan { get; set; }

        // Danh sách ID của thưởng
        public List<int>? ThuongIds { get; set; } = new List<int>();

        // Danh sách ID của phạt
        public List<int>? PhatIds { get; set; } = new List<int>();

        // Danh sách ID của chấm công
/*        public List<int>? ChamCongIds { get; set; } = new List<int>();*/

        public double? SoNgayNghiCoPhep { get; set; }
        public double? SoNgayNghiKhongPhep { get; set; }
    }

}
