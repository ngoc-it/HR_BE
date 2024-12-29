using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_BEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CongViec",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CongViecId = table.Column<int>(type: "int", nullable: false),
                    TenCongViec = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayHoanThanh = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CongViec", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhanCong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhanCongId = table.Column<int>(type: "int", nullable: false),
                    CongViecId = table.Column<int>(type: "int", nullable: false),
                    NhanVienId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NhanVienId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanCong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhanCong_CongViec_CongViecId",
                        column: x => x.CongViecId,
                        principalTable: "CongViec",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhanCong_NhanViens_NhanVienId1",
                        column: x => x.NhanVienId1,
                        principalTable: "NhanViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhanCong_CongViecId",
                table: "PhanCong",
                column: "CongViecId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCong_NhanVienId1",
                table: "PhanCong",
                column: "NhanVienId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhanCong");

            migrationBuilder.DropTable(
                name: "CongViec");
        }
    }
}
