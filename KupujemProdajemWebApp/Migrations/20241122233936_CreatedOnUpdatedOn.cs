using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KupujemProdajemWebApp.Migrations
{
    /// <inheritdoc />
    public partial class CreatedOnUpdatedOn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "948a66c0-5930-41d5-b8a5-ae6a30453e4e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf457e0d-9222-4715-a6ff-ebe0aefae120");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Advertisements",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13ed3501-11b4-45e9-90df-0a1c8dbe22be", null, "User", "USER" },
                    { "152ecc45-0d2a-4933-83ff-45a87d666d96", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13ed3501-11b4-45e9-90df-0a1c8dbe22be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "152ecc45-0d2a-4933-83ff-45a87d666d96");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Advertisements");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "948a66c0-5930-41d5-b8a5-ae6a30453e4e", null, "User", "USER" },
                    { "cf457e0d-9222-4715-a6ff-ebe0aefae120", null, "Admin", "ADMIN" }
                });
        }
    }
}
