﻿// <auto-generated />
using System;
using HR_BEND.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HR_BEND.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241117061828_UpdateDatabase16")]
    partial class UpdateDatabase16
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HR_BEND.Models.Data.ChamCong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CheckInTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CheckOutTime")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<DateTime>("NgayChamCong")
                        .HasColumnType("datetime2");

                    b.Property<int>("NhanVienID")
                        .HasColumnType("int");

                    b.Property<string>("ViTriCheckIn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ViTriCheckOut")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NhanVienID");

                    b.ToTable("ChamCongs");
                });

            modelBuilder.Entity("HR_BEND.Models.Data.ChucVu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChucVuID")
                        .HasColumnType("int");

                    b.Property<string>("TenChucVu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChucVus");
                });

            modelBuilder.Entity("HR_BEND.Models.Data.CongViec", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CongViecId")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayHoanThanh")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHoanThanhDuKien")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<int>("NguoiTaoId")
                        .HasColumnType("int");

                    b.Property<string>("TenCongViec")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NguoiTaoId");

                    b.ToTable("CongViec");
                });

            modelBuilder.Entity("HR_BEND.Models.Data.DanhGia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DanhGiaId")
                        .HasColumnType("int");

                    b.Property<float>("DiemDanhGia")
                        .HasColumnType("real");

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NguoiDanhGiaId")
                        .HasColumnType("int");

                    b.Property<int>("PhanCongId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NguoiDanhGiaId");

                    b.HasIndex("PhanCongId");

                    b.ToTable("DanhGias");
                });

            modelBuilder.Entity("HR_BEND.Models.Data.NhanVien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Anh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ChucVuId")
                        .HasColumnType("int");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GioiTinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayVaoLam")
                        .HasColumnType("datetime2");

                    b.Property<string>("NhanVienID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhongBanId")
                        .HasColumnType("int");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrangThaiID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChucVuId");

                    b.HasIndex("PhongBanId");

                    b.ToTable("NhanViens");
                });

            modelBuilder.Entity("HR_BEND.Models.Data.PhanCong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CongViecId")
                        .HasColumnType("int");

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHoanThanh")
                        .HasColumnType("datetime2");

                    b.Property<int>("NguoiDuocPhanCongId")
                        .HasColumnType("int");

                    b.Property<int>("NguoiPhanCongId")
                        .HasColumnType("int");

                    b.Property<int>("PhanCongId")
                        .HasColumnType("int");

                    b.Property<string>("TenCongViecPhanCong")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CongViecId");

                    b.HasIndex("NguoiDuocPhanCongId");

                    b.HasIndex("NguoiPhanCongId");

                    b.ToTable("PhanCongs");
                });

            modelBuilder.Entity("HR_BEND.Models.Data.PhongBan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PhongBanID")
                        .HasColumnType("int");

                    b.Property<string>("TenPhongBan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PhongBans");
                });

            modelBuilder.Entity("HR_BEND.Models.Data.PhuCap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChucVuID")
                        .HasColumnType("int");

                    b.Property<int>("PhuCapID")
                        .HasColumnType("int");

                    b.Property<float>("SoTien")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ChucVuID");

                    b.ToTable("PhuCaps");
                });

            modelBuilder.Entity("HR_BEND.Models.Data.TaiKhoan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChucVuId")
                        .HasColumnType("int");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NhanVienId")
                        .HasColumnType("int");

                    b.Property<string>("TenDangNhap")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrangThai")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChucVuId");

                    b.HasIndex("NhanVienId");

                    b.ToTable("TaiKhoans");
                });

            modelBuilder.Entity("HR_BEND.Models.Data.ChamCong", b =>
                {
                    b.HasOne("HR_BEND.Models.Data.NhanVien", "NhanVien")
                        .WithMany()
                        .HasForeignKey("NhanVienID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NhanVien");
                });

            modelBuilder.Entity("HR_BEND.Models.Data.CongViec", b =>
                {
                    b.HasOne("HR_BEND.Models.Data.NhanVien", "NhanVien")
                        .WithMany()
                        .HasForeignKey("NguoiTaoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("NhanVien");
                });

            modelBuilder.Entity("HR_BEND.Models.Data.DanhGia", b =>
                {
                    b.HasOne("HR_BEND.Models.Data.NhanVien", "NguoiDanhGia")
                        .WithMany()
                        .HasForeignKey("NguoiDanhGiaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HR_BEND.Models.Data.PhanCong", "PhanCong")
                        .WithMany()
                        .HasForeignKey("PhanCongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDanhGia");

                    b.Navigation("PhanCong");
                });

            modelBuilder.Entity("HR_BEND.Models.Data.NhanVien", b =>
                {
                    b.HasOne("HR_BEND.Models.Data.ChucVu", "ChucVu")
                        .WithMany()
                        .HasForeignKey("ChucVuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HR_BEND.Models.Data.PhongBan", "PhongBan")
                        .WithMany()
                        .HasForeignKey("PhongBanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChucVu");

                    b.Navigation("PhongBan");
                });

            modelBuilder.Entity("HR_BEND.Models.Data.PhanCong", b =>
                {
                    b.HasOne("HR_BEND.Models.Data.CongViec", "CongViec")
                        .WithMany()
                        .HasForeignKey("CongViecId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HR_BEND.Models.Data.NhanVien", "NguoiDuocPhanCong")
                        .WithMany()
                        .HasForeignKey("NguoiDuocPhanCongId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HR_BEND.Models.Data.NhanVien", "NguoiPhanCong")
                        .WithMany()
                        .HasForeignKey("NguoiPhanCongId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CongViec");

                    b.Navigation("NguoiDuocPhanCong");

                    b.Navigation("NguoiPhanCong");
                });

            modelBuilder.Entity("HR_BEND.Models.Data.PhuCap", b =>
                {
                    b.HasOne("HR_BEND.Models.Data.ChucVu", "ChucVu")
                        .WithMany()
                        .HasForeignKey("ChucVuID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChucVu");
                });

            modelBuilder.Entity("HR_BEND.Models.Data.TaiKhoan", b =>
                {
                    b.HasOne("HR_BEND.Models.Data.ChucVu", "ChucVu")
                        .WithMany()
                        .HasForeignKey("ChucVuId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HR_BEND.Models.Data.NhanVien", "NhanVien")
                        .WithMany()
                        .HasForeignKey("NhanVienId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ChucVu");

                    b.Navigation("NhanVien");
                });
#pragma warning restore 612, 618
        }
    }
}
