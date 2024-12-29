using System.ComponentModel.DataAnnotations;

namespace HR_BEND.Models.Data
{
    public class TaiKhoan : BaseData
    {
        public string TenDangNhap { get; set; }

        public string MatKhau { get; set; }
        public int NhanVienId { get; set; }
        public string ?TrangThai {  get; set; }
        public ChucVu ? ChucVu {  get; set; }
        public int ChucVuId { get; set; }
        public NhanVien ? NhanVien {  get; set; }


    }
    public class TaiKhoanCRUDModel
    {
        public string TenDangNhap { get; set; }

        public string MatKhau { get; set; }
        public int NhanVienId { get; set; }
        public int ChucVuId { get; set; }
        public string? TrangThai { get; set; }

    }

    public class LoginModel
    {
        [Required]
        public string TenDangNhap { get; set; }
        [Required]
        public string MatKhau { get; set; }
    }

}
