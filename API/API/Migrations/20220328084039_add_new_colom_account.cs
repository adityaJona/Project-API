using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add_new_colom_account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EkspiredToken",
                table: "ACCOUNT",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "OTP",
                table: "ACCOUNT",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isUsed",
                table: "ACCOUNT",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EkspiredToken",
                table: "ACCOUNT");

            migrationBuilder.DropColumn(
                name: "OTP",
                table: "ACCOUNT");

            migrationBuilder.DropColumn(
                name: "isUsed",
                table: "ACCOUNT");
        }
    }
}
