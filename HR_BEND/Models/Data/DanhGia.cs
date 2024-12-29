namespace HR_BEND.Models.Data
{
    public class DanhGia : BaseData
    {
        public int DanhGiaId { get; set; }
        public int PhanCongId { get; set; }

        public float DiemDanhGia {  get; set; }
        public DateTime? NgayDanhGia { get; set; }
        public string ? GhiChu {  get; set; }
        
        public PhanCong ? PhanCong {  get; set; }
        public int NguoiDanhGiaId { get; set; }
        public NhanVien ? NguoiDanhGia { get; set; }
    }
    public class DanhGiaCRUDModel {
        public int DanhGiaId { get; set; }
        public int PhanCongId { get; set; }
        public DateTime? NgayDanhGia { get; set; }
        public float DiemDanhGia { get; set; }
        public string? GhiChu { get; set; }
        public int NguoiDanhGiaId { get; set; }
    }

}
