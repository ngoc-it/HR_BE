using Microsoft.EntityFrameworkCore;

namespace HR_BEND.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<PhongBan> PhongBans { get; set; }
        public DbSet<ChucVu> ChucVus { get; set; }
        public DbSet<PhuCap> PhuCaps { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<CongViec> CongViec { get; set;}

        public DbSet<PhanCong> PhanCongs { get; set;}
        public DbSet<ChamCong> ChamCongs { get; set;}
        public DbSet<DanhGia> DanhGias { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }

        public DbSet<HopDong> HopDongs { get; set; }
        public DbSet<Thuong> Thuongs { get; set; }
        public DbSet<Phat> Phats { get; set; }
        public DbSet<TinhLuong> TinhLuongs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Định nghĩa mối quan hệ giữa CongViec và NhanVien
            modelBuilder.Entity<CongViec>()
                .HasOne(c => c.NhanVien)  // CongViec có một NhanVien
                .WithMany()  // Mỗi NhanVien có thể có nhiều CongViec
                .HasForeignKey(c => c.NguoiTaoId)  // Khóa ngoại là NguoiTaoId
                .OnDelete(DeleteBehavior.Restrict);  // Bạn có thể thay đổi hành vi xóa nếu cần

            modelBuilder.Entity<PhanCong>()
       .HasOne(p => p.NguoiPhanCong)
       .WithMany()
       .HasForeignKey(p => p.NguoiPhanCongId)
       .OnDelete(DeleteBehavior.NoAction); // Không sử dụng Cascade Delete

            modelBuilder.Entity<PhanCong>()
                .HasOne(p => p.NguoiDuocPhanCong)
                .WithMany()
                .HasForeignKey(p => p.NguoiDuocPhanCongId)
                .OnDelete(DeleteBehavior.NoAction); // Không sử dụng Cascade Delete

            // Cấu hình liên kết giữa PhanCong và CongViec
            modelBuilder.Entity<PhanCong>()
                .HasOne(p => p.CongViec)
                .WithMany()
                .HasForeignKey(p => p.CongViecId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TaiKhoan>()
       .HasOne(t => t.NhanVien)
       .WithMany()
       .HasForeignKey(t => t.NhanVienId)
       .OnDelete(DeleteBehavior.NoAction); // Không xóa cascade

            modelBuilder.Entity<TaiKhoan>()
                .HasOne(t => t.ChucVu)
                .WithMany()
                .HasForeignKey(t => t.ChucVuId)
                .OnDelete(DeleteBehavior.NoAction); // Không xóa cascadev




            // Cấu hình các mối quan hệ
            modelBuilder.Entity<TinhLuong>()
                .HasOne(tl => tl.HopDong)
                .WithMany()
                .HasForeignKey(tl => tl.HopDongId)
                .OnDelete(DeleteBehavior.Cascade);  // Xóa Cascade cho HopDong

            modelBuilder.Entity<TinhLuong>()
                .HasOne(tl => tl.NhanVien)
                .WithMany()
                .HasForeignKey(tl => tl.NhanVienId)
                .OnDelete(DeleteBehavior.NoAction);  // Không xóa Cascade cho NhanVien để tránh xung đột


          

        }


    }

}
