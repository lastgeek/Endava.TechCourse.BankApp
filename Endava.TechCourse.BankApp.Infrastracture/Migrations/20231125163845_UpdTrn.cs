using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Endava.TechCourse.BankApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdTrn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "DestinationWalletId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SourceWalletId",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "DestinationWalletCode",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SourceWalletCode",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("bef2745e-ef94-49ee-84ec-1d5f7c832df9"), null, "Admin", "Admin" },
                    { new Guid("fbf36315-8190-49ed-8de0-fd09f1960bc1"), null, "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bef2745e-ef94-49ee-84ec-1d5f7c832df9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fbf36315-8190-49ed-8de0-fd09f1960bc1"));

            migrationBuilder.DropColumn(
                name: "DestinationWalletCode",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SourceWalletCode",
                table: "Transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "DestinationWalletId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SourceWalletId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("48b517d1-5e44-4ca4-83b0-f1b5db14944d"), null, "User", "User" },
                    { new Guid("4c0b04d4-b39e-4f36-a4e8-2e84671386a5"), null, "Admin", "Admin" }
                });
        }
    }
}
