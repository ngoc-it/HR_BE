using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_BEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateData24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TinhLuongs_ChamCongs_ChamCongId",
                table: "TinhLuongs");

            migrationBuilder.DropIndex(
                name: "IX_TinhLuongs_ChamCongId",
                table: "TinhLuongs");

            migrationBuilder.DropColumn(
                name: "ChamCongId",
                table: "TinhLuongs");

            migrationBuilder.AddColumn<string>(
                name: "ChamCongIds",
                table: "TinhLuongs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<int>(
                name: "TinhLuongId",
                table: "ChamCongs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChamCongs_TinhLuongId",
                table: "ChamCongs",
                column: "TinhLuongId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChamCongs_TinhLuongs_TinhLuongId",
                table: "ChamCongs",
                column: "TinhLuongId",
                principalTable: "TinhLuongs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChamCongs_TinhLuongs_TinhLuongId",
                table: "ChamCongs");

            migrationBuilder.DropIndex(
                name: "IX_ChamCongs_TinhLuongId",
                table: "ChamCongs");

            migrationBuilder.DropColumn(
                name: "ChamCongIds",
                table: "TinhLuongs");

            migrationBuilder.DropColumn(
                name: "TinhLuongId",
                table: "ChamCongs");

            migrationBuilder.AddColumn<int>(
                name: "ChamCongId",
                table: "TinhLuongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TinhLuongs_ChamCongId",
                table: "TinhLuongs",
                column: "ChamCongId");

            migrationBuilder.AddForeignKey(
                name: "FK_TinhLuongs_ChamCongs_ChamCongId",
                table: "TinhLuongs",
                column: "ChamCongId",
                principalTable: "ChamCongs",
                principalColumn: "Id");
        }
    }
}
