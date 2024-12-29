using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_BEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateData14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhGias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DanhGiaId = table.Column<int>(type: "int", nullable: false),
                    PhanCongId = table.Column<int>(type: "int", nullable: false),
                    DiemDanhGia = table.Column<float>(type: "real", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguoiDanhGiaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DanhGias_NhanViens_NguoiDanhGiaId",
                        column: x => x.NguoiDanhGiaId,
                        principalTable: "NhanViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhGias_PhanCongs_PhanCongId",
                        column: x => x.PhanCongId,
                        principalTable: "PhanCongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DanhGias_NguoiDanhGiaId",
                table: "DanhGias",
                column: "NguoiDanhGiaId");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGias_PhanCongId",
                table: "DanhGias",
                column: "PhanCongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhGias");
        }
    }
}
