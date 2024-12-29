using System.ComponentModel.DataAnnotations.Schema;

namespace HR_BEND.Models.Data
{
    public class PhanCong : BaseData
    {

            public int PhanCongId { get; set; }
            public int CongViecId { get; set; }
            public string TenCongViecPhanCong { get; set; }
            public int NguoiDuocPhanCongId { get; set; }
            public int NguoiPhanCongId { get; set; }
            public DateTime? NgayBatDau { get; set; }
            public DateTime? NgayHoanThanh { get; set; }
            public string TrangThai { get; set; }
            public string? GhiChu { get; set; }
        // Liên kết với bảng NhanVien (nhân viên phân công và nhân viên được phân công)
        public NhanVien? NguoiDuocPhanCong { get; set; }
        public NhanVien? NguoiPhanCong { get; set; }

        public CongViec ? CongViec { get; set; }
        

    }
    public class PhanCongCRUDModel
    {
        public int PhanCongId { get; set; }
        public int CongViecId { get; set; }
        public string TenCongViecPhanCong { get; set; }
        public int NguoiDuocPhanCongId { get; set; }
        public int NguoiPhanCongId { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
        public string TrangThai { get; set; }
        public string? GhiChu { get; set; }

    }
    
}