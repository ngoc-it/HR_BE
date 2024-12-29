namespace HR_BEND.Models.Data
{
    public class CongViec :BaseData
    {
        public int CongViecId { get; set; }
        public string TenCongViec { get; set; }

        public string ?MoTa { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime ?NgayHoanThanhDuKien { get; set; }
        public DateTime ?NgayHoanThanh {  get; set; }
        public int NguoiTaoId { get; set; }
        public NhanVien? NhanVien { get; set; }
    }
    public class CongViecCRUDModel
    {
        public int CongViecId { get; set; }
        public string TenCongViec { get; set; }

        public string? MoTa { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayHoanThanhDuKien { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
        public int NguoiTaoId { get; set; }
    }


}