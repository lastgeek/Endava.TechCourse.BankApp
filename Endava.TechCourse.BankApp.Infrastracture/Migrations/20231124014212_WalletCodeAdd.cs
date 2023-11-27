using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Endava.TechCourse.BankApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WalletCodeAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0ea148e2-2ecd-4845-808f-29578b177d3b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e9be394f-f8cb-4b56-ae58-f6e81d3d1939"));

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Wallets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("48b517d1-5e44-4ca4-83b0-f1b5db14944d"), null, "User", "User" },
                    { new Guid("4c0b04d4-b39e-4f36-a4e8-2e84671386a5"), null, "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("48b517d1-5e44-4ca4-83b0-f1b5db14944d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4c0b04d4-b39e-4f36-a4e8-2e84671386a5"));

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Wallets");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0ea148e2-2ecd-4845-808f-29578b177d3b"), null, "Admin", "Admin" },
                    { new Guid("e9be394f-f8cb-4b56-ae58-f6e81d3d1939"), null, "User", "User" }
                });
        }
    }
}
