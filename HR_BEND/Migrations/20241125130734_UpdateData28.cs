using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_BEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateData28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TinhLuongs_PhuCaps_PhuCapId",
                table: "TinhLuongs");

            migrationBuilder.DropIndex(
                name: "IX_TinhLuongs_PhuCapId",
                table: "TinhLuongs");

            migrationBuilder.DropColumn(
                name: "PhuCapId",
                table: "TinhLuongs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhuCapId",
                table: "TinhLuongs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TinhLuongs_PhuCapId",
                table: "TinhLuongs",
                column: "PhuCapId");

            migrationBuilder.AddForeignKey(
                name: "FK_TinhLuongs_PhuCaps_PhuCapId",
                table: "TinhLuongs",
                column: "PhuCapId",
                principalTable: "PhuCaps",
                principalColumn: "Id");
        }
    }
}
