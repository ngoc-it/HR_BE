using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_BEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateData9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NguoiDuocPhanCongId",
                table: "PhanCongs");

            migrationBuilder.DropColumn(
                name: "NguoiPhanCongId",
                table: "PhanCongs");

            migrationBuilder.AddColumn<string>(
                name: "NguoiPhanCong",
                table: "PhanCongs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NguoiPhanCong",
                table: "PhanCongs");

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
        }
    }
}
