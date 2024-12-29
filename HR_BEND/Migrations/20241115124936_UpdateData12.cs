using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_BEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateData12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhanCongs_CongViec_CongViecId",
                table: "PhanCongs");

            migrationBuilder.DropForeignKey(
                name: "FK_PhanCongs_NhanViens_NhanVienId",
                table: "PhanCongs");

            migrationBuilder.DropIndex(
                name: "IX_PhanCongs_NhanVienId",
                table: "PhanCongs");

            migrationBuilder.DropColumn(
                name: "NhanVienId",
                table: "PhanCongs");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCongs_NguoiDuocPhanCongId",
                table: "PhanCongs",
                column: "NguoiDuocPhanCongId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCongs_NguoiPhanCongId",
                table: "PhanCongs",
                column: "NguoiPhanCongId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhanCongs_CongViec_CongViecId",
                table: "PhanCongs",
                column: "CongViecId",
                principalTable: "CongViec",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhanCongs_NhanViens_NguoiDuocPhanCongId",
                table: "PhanCongs",
                column: "NguoiDuocPhanCongId",
                principalTable: "NhanViens",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhanCongs_NhanViens_NguoiPhanCongId",
                table: "PhanCongs",
                column: "NguoiPhanCongId",
                principalTable: "NhanViens",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhanCongs_CongViec_CongViecId",
                table: "PhanCongs");

            migrationBuilder.DropForeignKey(
                name: "FK_PhanCongs_NhanViens_NguoiDuocPhanCongId",
                table: "PhanCongs");

            migrationBuilder.DropForeignKey(
                name: "FK_PhanCongs_NhanViens_NguoiPhanCongId",
                table: "PhanCongs");

            migrationBuilder.DropIndex(
                name: "IX_PhanCongs_NguoiDuocPhanCongId",
                table: "PhanCongs");

            migrationBuilder.DropIndex(
                name: "IX_PhanCongs_NguoiPhanCongId",
                table: "PhanCongs");

            migrationBuilder.AddColumn<int>(
                name: "NhanVienId",
                table: "PhanCongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PhanCongs_NhanVienId",
                table: "PhanCongs",
                column: "NhanVienId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhanCongs_CongViec_CongViecId",
                table: "PhanCongs",
                column: "CongViecId",
                principalTable: "CongViec",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhanCongs_NhanViens_NhanVienId",
                table: "PhanCongs",
                column: "NhanVienId",
                principalTable: "NhanViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
