using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Endava.TechCourse.BankApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdTrnsc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("39db146b-b4b5-4758-90ff-8cb443e28215"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("494ac6ba-88e4-46e6-acce-5856181e4f93"));

            migrationBuilder.AddColumn<decimal>(
                name: "Commission",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4cc0fc5e-6526-4c90-a339-0d12ab78febf"), null, "User", "User" },
                    { new Guid("5f9422a3-c368-481c-a988-c8df2f34621c"), null, "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4cc0fc5e-6526-4c90-a339-0d12ab78febf"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5f9422a3-c368-481c-a988-c8df2f34621c"));

            migrationBuilder.DropColumn(
                name: "Commission",
                table: "Transactions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("39db146b-b4b5-4758-90ff-8cb443e28215"), null, "Admin", "Admin" },
                    { new Guid("494ac6ba-88e4-46e6-acce-5856181e4f93"), null, "User", "User" }
                });
        }
    }
}
