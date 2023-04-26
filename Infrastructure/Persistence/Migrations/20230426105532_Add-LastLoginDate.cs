using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLastLoginDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c24e743-442d-43cc-85b7-5d7a5f69ee40");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae41d58a-fde1-4516-a52a-2967b1889d90");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e44eba1c-3614-4191-882e-d8c6b15cd802");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "400c89c8-17d5-4abc-b9cf-78f0e51321da", null, "Member", "MEMBER" },
                    { "9308ab5c-11bf-48a0-896c-e4165892d2ca", null, "Administrator", "ADMINISTRATOR" },
                    { "9ca3b1e6-aee5-460f-ae4a-704960edd7e3", null, "Guest", "GUEST" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "400c89c8-17d5-4abc-b9cf-78f0e51321da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9308ab5c-11bf-48a0-896c-e4165892d2ca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ca3b1e6-aee5-460f-ae4a-704960edd7e3");

            migrationBuilder.DropColumn(
                name: "LastLoginDate",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c24e743-442d-43cc-85b7-5d7a5f69ee40", null, "Administrator", "ADMINISTRATOR" },
                    { "ae41d58a-fde1-4516-a52a-2967b1889d90", null, "Guest", "GUEST" },
                    { "e44eba1c-3614-4191-882e-d8c6b15cd802", null, "Member", "MEMBER" }
                });
        }
    }
}
