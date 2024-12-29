namespace HR_BEND.Models.Data
{
    public class ChamCong : BaseData
    {
        public int NhanVienID { get; set; } // Liên kết với bảng NhanVien
        public DateTime? NgayChamCong { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string? ViTriCheckIn { get; set; } // Địa chỉ hoặc mô tả vị trí khi check-in
        public string? ViTriCheckOut { get; set; } // Địa chỉ hoặc mô tả vị trí khi check-out

        public NhanVien ?NhanVien { get; set; }
    }
    public class ChamCongCRUDModel {
        public int Id { get; set; }
        public int NhanVienID { get; set; } // Liên kết với bảng NhanVien
        public DateTime? NgayChamCong { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string? ViTriCheckIn { get; set; } // Địa chỉ hoặc mô tả vị trí khi check-in
        public string? ViTriCheckOut { get; set; } // Địa chỉ hoặc mô tả vị trí khi check-out
        public string ?LyDo {  get; set; }
    }


}
