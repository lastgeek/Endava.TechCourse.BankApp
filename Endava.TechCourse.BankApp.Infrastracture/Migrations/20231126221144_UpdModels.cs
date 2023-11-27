using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Endava.TechCourse.BankApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b78fcdb9-4012-496a-97b7-1b0e0963906b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c55d8481-e5ba-4881-bf62-b6db6492592a"));

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Wallets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanBeRemoved",
                table: "Currency",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("9d9b403b-762e-4c30-aa6b-c9b3fe01c148"), null, "Admin", "Admin" },
                    { new Guid("aed05efa-1146-4e7c-bb96-3f737343fd87"), null, "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9d9b403b-762e-4c30-aa6b-c9b3fe01c148"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("aed05efa-1146-4e7c-bb96-3f737343fd87"));

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "CanBeRemoved",
                table: "Currency");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("b78fcdb9-4012-496a-97b7-1b0e0963906b"), null, "User", "User" },
                    { new Guid("c55d8481-e5ba-4881-bf62-b6db6492592a"), null, "Admin", "Admin" }
                });
        }
    }
}
