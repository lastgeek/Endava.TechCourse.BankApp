using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Endava.TechCourse.BankApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdWalletType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Type",
                table: "Wallets");

            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                table: "Wallets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "WalletType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Commision = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CanBeRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("39db146b-b4b5-4758-90ff-8cb443e28215"), null, "Admin", "Admin" },
                    { new Guid("494ac6ba-88e4-46e6-acce-5856181e4f93"), null, "User", "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_TypeId",
                table: "Wallets",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_WalletType_TypeId",
                table: "Wallets",
                column: "TypeId",
                principalTable: "WalletType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_WalletType_TypeId",
                table: "Wallets");

            migrationBuilder.DropTable(
                name: "WalletType");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_TypeId",
                table: "Wallets");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("39db146b-b4b5-4758-90ff-8cb443e28215"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("494ac6ba-88e4-46e6-acce-5856181e4f93"));

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Wallets");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Wallets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("9d9b403b-762e-4c30-aa6b-c9b3fe01c148"), null, "Admin", "Admin" },
                    { new Guid("aed05efa-1146-4e7c-bb96-3f737343fd87"), null, "User", "User" }
                });
        }
    }
}
