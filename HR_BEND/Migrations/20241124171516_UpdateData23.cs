﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_BEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateData23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "LuongThucNhan",
                table: "TinhLuongs",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "LuongThucNhan",
                table: "TinhLuongs",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
