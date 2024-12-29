using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_BEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateData10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NguoiPhanCong",
                table: "PhanCongs");

            migrationBuilder.DropColumn(
                name: "NguoiTao",
                table: "CongViec");

            migrationBuilder.AddColumn<int>(
                name: "NguoiDuocPhanCongId",
                table: "PhanCongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NguoiPhanCongId",
                table: "PhanCongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NguoiTaoId",
                table: "CongViec",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CongViec_NguoiTaoId",
                table: "CongViec",
                column: "NguoiTaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CongViec_NhanViens_NguoiTaoId",
                table: "CongViec",
                column: "NguoiTaoId",
                principalTable: "NhanViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CongViec_NhanViens_NguoiTaoId",
                table: "CongViec");

            migrationBuilder.DropIndex(
                name: "IX_CongViec_NguoiTaoId",
                table: "CongViec");

            migrationBuilder.DropColumn(
                name: "NguoiDuocPhanCongId",
                table: "PhanCongs");

            migrationBuilder.DropColumn(
                name: "NguoiPhanCongId",
                table: "PhanCongs");

            migrationBuilder.DropColumn(
                name: "NguoiTaoId",
                table: "CongViec");

            migrationBuilder.AddColumn<string>(
                name: "NguoiPhanCong",
                table: "PhanCongs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NguoiTao",
                table: "CongViec",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
