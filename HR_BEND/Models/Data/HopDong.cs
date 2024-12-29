namespace HR_BEND.Models.Data
{
    public class HopDong : BaseData
    {
        public int HopDongId { get; set; }
        public string TenHopDong { get; set; }

        public int NhanVienId { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime ? NgayKetThuc   {get;set;}
        public double ? LuongCoBan { get; set; }
        public string ?TrangThai {  get; set; }
        public string ? GhiChu {  get; set; }

        public NhanVien ? NhanVien {  get; set; }
    }
    public class HopDongCRUDModel {
        public int HopDongId { get; set; }
        public string TenHopDong { get; set; }

        public int NhanVienId { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public double? LuongCoBan { get; set; }
        public string? TrangThai { get; set; }
        public string? GhiChu { get; set; }
    }
}
