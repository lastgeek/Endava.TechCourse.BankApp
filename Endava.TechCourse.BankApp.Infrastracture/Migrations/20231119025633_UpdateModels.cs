using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Endava.TechCourse.BankApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0fd3aa9c-882b-4989-b121-d1b70b4a0b1a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4aec3ca2-9b81-45ca-bf72-20b07f630abb"));

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "DestinationUserName",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SourceUserName",
                table: "Transactions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7026f9b1-1d65-4e94-85e3-f5b8c25e9403"), null, "Admin", "Admin" },
                    { new Guid("715e613c-d2e5-4748-9f25-1f53a9670963"), null, "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7026f9b1-1d65-4e94-85e3-f5b8c25e9403"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("715e613c-d2e5-4748-9f25-1f53a9670963"));

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Wallets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DestinationUserName",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SourceUserName",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0fd3aa9c-882b-4989-b121-d1b70b4a0b1a"), null, "Admin", "Admin" },
                    { new Guid("4aec3ca2-9b81-45ca-bf72-20b07f630abb"), null, "User", "User" }
                });
        }
    }
}
