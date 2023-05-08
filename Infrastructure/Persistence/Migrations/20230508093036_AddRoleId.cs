using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "056c7c6d-2be9-48ea-926a-da42728f1cc7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "390cde8a-6582-48dd-8632-c168488fb947");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8cad7b3-920b-478d-b617-1eb82daf2846");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "052997d8-20f2-4089-b5e1-50b296461d51", null, "Member", "MEMBER" },
                    { "d681023c-1441-4134-8253-f660cf26a716", null, "Guest", "GUEST" },
                    { "f0967a26-d5ba-41a8-92e5-747783c4cc07", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "052997d8-20f2-4089-b5e1-50b296461d51");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d681023c-1441-4134-8253-f660cf26a716");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0967a26-d5ba-41a8-92e5-747783c4cc07");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "056c7c6d-2be9-48ea-926a-da42728f1cc7", null, "Administrator", "ADMINISTRATOR" },
                    { "390cde8a-6582-48dd-8632-c168488fb947", null, "Member", "MEMBER" },
                    { "d8cad7b3-920b-478d-b617-1eb82daf2846", null, "Guest", "GUEST" }
                });
        }
    }
}
