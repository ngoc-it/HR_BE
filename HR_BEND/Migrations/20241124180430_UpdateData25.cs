using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_BEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateData25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TinhLuongs_Phats_PhatId",
                table: "TinhLuongs");

            migrationBuilder.DropForeignKey(
                name: "FK_TinhLuongs_Thuongs_ThuongId",
                table: "TinhLuongs");

            migrationBuilder.DropIndex(
                name: "IX_TinhLuongs_PhatId",
                table: "TinhLuongs");

            migrationBuilder.DropIndex(
                name: "IX_TinhLuongs_ThuongId",
                table: "TinhLuongs");

            migrationBuilder.DropColumn(
                name: "PhatId",
                table: "TinhLuongs");

            migrationBuilder.DropColumn(
                name: "ThuongId",
                table: "TinhLuongs");

            migrationBuilder.AlterColumn<int>(
                name: "PhuCapId",
                table: "TinhLuongs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "PhatIds",
                table: "TinhLuongs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThuongIds",
                table: "TinhLuongs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhatIds",
                table: "TinhLuongs");

            migrationBuilder.DropColumn(
                name: "ThuongIds",
                table: "TinhLuongs");

            migrationBuilder.AlterColumn<int>(
                name: "PhuCapId",
                table: "TinhLuongs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhatId",
                table: "TinhLuongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThuongId",
                table: "TinhLuongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TinhLuongs_PhatId",
                table: "TinhLuongs",
                column: "PhatId");

            migrationBuilder.CreateIndex(
                name: "IX_TinhLuongs_ThuongId",
                table: "TinhLuongs",
                column: "ThuongId");

            migrationBuilder.AddForeignKey(
                name: "FK_TinhLuongs_Phats_PhatId",
                table: "TinhLuongs",
                column: "PhatId",
                principalTable: "Phats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TinhLuongs_Thuongs_ThuongId",
                table: "TinhLuongs",
                column: "ThuongId",
                principalTable: "Thuongs",
                principalColumn: "Id");
        }
    }
}
