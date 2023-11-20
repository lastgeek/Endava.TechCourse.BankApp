using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Endava.TechCourse.BankApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWalletModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1799cb9e-4738-4ca6-9efd-1b122865cdb9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b42c44b0-42e3-4877-9f0d-979e0429721e"));

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Wallets",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1799cb9e-4738-4ca6-9efd-1b122865cdb9"), null, "Admin", "Admin" },
                    { new Guid("b42c44b0-42e3-4877-9f0d-979e0429721e"), null, "User", "User" }
                });
        }
    }
}
