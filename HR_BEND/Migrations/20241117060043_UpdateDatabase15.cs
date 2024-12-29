using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_BEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChamCongID",
                table: "ChamCongs");

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NhanVienId = table.Column<int>(type: "int", nullable: false),
                    ChucVuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaiKhoans_ChucVus_ChucVuId",
                        column: x => x.ChucVuId,
                        principalTable: "ChucVus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaiKhoans_NhanViens_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "NhanViens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_ChucVuId",
                table: "TaiKhoans",
                column: "ChucVuId");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_NhanVienId",
                table: "TaiKhoans",
                column: "NhanVienId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaiKhoans");

            migrationBuilder.AddColumn<int>(
                name: "ChamCongID",
                table: "ChamCongs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
