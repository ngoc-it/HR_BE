namespace HR_BEND.Models.Data
{
    public class PhuCap : BaseData
    {
        public int PhuCapID { get; set; }
        public int ChucVuID { get; set; }
        public double SoTien { get; set; }
        public ChucVu ? ChucVu { get; set; }
    }
}
