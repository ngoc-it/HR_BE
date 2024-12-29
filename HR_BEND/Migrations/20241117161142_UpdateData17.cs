﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_BEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateData17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "ChamCongs");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "ChamCongs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayChamCong",
                table: "ChamCongs",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayChamCong",
                table: "ChamCongs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "ChamCongs",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "ChamCongs",
                type: "float",
                nullable: true);
        }
    }
}
