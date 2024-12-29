using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_BEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateData21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
