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
    [Migration("20241113125403_UpdateData6")]
    partial class UpdateData6
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.Property<string>("NguoiTao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenCongViec")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CongViec");
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

                    b.Property<DateTime>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHoanThanh")
                        .HasColumnType("datetime2");

                    b.Property<int>("NguoiDuocPhanCongId")
                        .HasColumnType("int");

                    b.Property<int>("NguoiPhanCongId")
                        .HasColumnType("int");

                    b.Property<int>("NhanVienId")
                        .HasColumnType("int");

                    b.Property<int>("PhanCongId")
                        .HasColumnType("int");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CongViecId");

                    b.HasIndex("NhanVienId");

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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HR_BEND.Models.Data.NhanVien", "NhanVien")
                        .WithMany()
                        .HasForeignKey("NhanVienId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CongViec");

                    b.Navigation("NhanVien");
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
#pragma warning restore 612, 618
        }
    }
}
