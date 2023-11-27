using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Endava.TechCourse.BankApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMainWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bef2745e-ef94-49ee-84ec-1d5f7c832df9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fbf36315-8190-49ed-8de0-fd09f1960bc1"));

            migrationBuilder.AddColumn<bool>(
                name: "MainWallet",
                table: "Wallets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("b78fcdb9-4012-496a-97b7-1b0e0963906b"), null, "User", "User" },
                    { new Guid("c55d8481-e5ba-4881-bf62-b6db6492592a"), null, "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b78fcdb9-4012-496a-97b7-1b0e0963906b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c55d8481-e5ba-4881-bf62-b6db6492592a"));

            migrationBuilder.DropColumn(
                name: "MainWallet",
                table: "Wallets");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("bef2745e-ef94-49ee-84ec-1d5f7c832df9"), null, "Admin", "Admin" },
                    { new Guid("fbf36315-8190-49ed-8de0-fd09f1960bc1"), null, "User", "User" }
                });
        }
    }
}
