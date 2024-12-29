namespace HR_BEND.Models.Data
{
    public class NhanVien : BaseData
    {
        public string NhanVienID { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string ?Anh { get; set; }
        public string Email { get; set; }
        public int PhongBanId { get; set; }
        public int ChucVuId { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public int TrangThaiID { get; set; }
        public string ? GioiTinh { get; set; } //giới tính
        public PhongBan PhongBan { get; set; }
        public ChucVu ChucVu { get; set; }

    }

    public class NhanVienCRUDModel
    {
        public string NhanVienID { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string? Anh { get; set; }
        public string Email { get; set; }
        public int PhongBanId { get; set; }
        public int ChucVuId { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public int TrangThaiID { get; set; }
        public string? GioiTinh { get; set; } //giới tính

        // Liên kết ngược tới PhanCong
        public ICollection<PhanCong>? PhanCongNguoiDuocPhanCong { get; set; }
        public ICollection<PhanCong>? PhanCongNguoiPhanCong { get; set; }

        public ICollection<DanhGia>? NguoiDanhGia {  get; set; }
    }
}
