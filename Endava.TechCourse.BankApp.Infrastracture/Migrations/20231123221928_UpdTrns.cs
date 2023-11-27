using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Endava.TechCourse.BankApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdTrns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0bda9b2d-51b4-4e0c-b5df-b7dcb23d5fbc"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("67c5d988-043e-4c36-b565-7cf2a5b92912"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0ea148e2-2ecd-4845-808f-29578b177d3b"), null, "Admin", "Admin" },
                    { new Guid("e9be394f-f8cb-4b56-ae58-f6e81d3d1939"), null, "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0ea148e2-2ecd-4845-808f-29578b177d3b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e9be394f-f8cb-4b56-ae58-f6e81d3d1939"));

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Transactions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0bda9b2d-51b4-4e0c-b5df-b7dcb23d5fbc"), null, "Admin", "Admin" },
                    { new Guid("67c5d988-043e-4c36-b565-7cf2a5b92912"), null, "User", "User" }
                });
        }
    }
}
