using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_BEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateData20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Phats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTien = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Thuongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTienThuong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTienThuong = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thuongs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TinhLuongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTinhLuong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThangNam = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoNgayCong = table.Column<double>(type: "float", nullable: false),
                    HopDongId = table.Column<int>(type: "int", nullable: false),
                    LuongThucNhan = table.Column<int>(type: "int", nullable: false),
                    NhanVienId = table.Column<int>(type: "int", nullable: false),
                    ThuongId = table.Column<int>(type: "int", nullable: false),
                    PhatId = table.Column<int>(type: "int", nullable: false),
                    PhuCapId = table.Column<int>(type: "int", nullable: false),
                    SoNgayNghiCoPhep = table.Column<double>(type: "float", nullable: true),
                    SoNgayNghiKhongPhep = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinhLuongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TinhLuongs_HopDongs_HopDongId",
                        column: x => x.HopDongId,
                        principalTable: "HopDongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TinhLuongs_NhanViens_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "NhanViens",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TinhLuongs_Phats_PhatId",
                        column: x => x.PhatId,
                        principalTable: "Phats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TinhLuongs_PhuCaps_PhuCapId",
                        column: x => x.PhuCapId,
                        principalTable: "PhuCaps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TinhLuongs_Thuongs_ThuongId",
                        column: x => x.ThuongId,
                        principalTable: "Thuongs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TinhLuongs_HopDongId",
                table: "TinhLuongs",
                column: "HopDongId");

            migrationBuilder.CreateIndex(
                name: "IX_TinhLuongs_NhanVienId",
                table: "TinhLuongs",
                column: "NhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_TinhLuongs_PhatId",
                table: "TinhLuongs",
                column: "PhatId");

            migrationBuilder.CreateIndex(
                name: "IX_TinhLuongs_PhuCapId",
                table: "TinhLuongs",
                column: "PhuCapId");

            migrationBuilder.CreateIndex(
                name: "IX_TinhLuongs_ThuongId",
                table: "TinhLuongs",
                column: "ThuongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TinhLuongs");

            migrationBuilder.DropTable(
                name: "Phats");

            migrationBuilder.DropTable(
                name: "Thuongs");
        }
    }
}
