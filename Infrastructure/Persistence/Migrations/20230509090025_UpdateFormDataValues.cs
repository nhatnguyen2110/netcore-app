using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFormDataValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GetSingleValueQueryString",
                columns: table => new
                {
                    val = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 1,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 2,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 3,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 4,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 5,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 6,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 7,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 8,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 9,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 10,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 11,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 12,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 13,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 14,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 15,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 16,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 17,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 18,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 19,
                column: "DBTable",
                value: "FormDatas");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 20,
                column: "DBTable",
                value: "FormDatas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetSingleValueQueryString");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 1,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 2,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 3,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 4,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 5,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 6,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 7,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 8,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 9,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 10,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 11,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 12,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 13,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 14,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 15,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 16,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 17,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 18,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 19,
                column: "DBTable",
                value: "FormData");

            migrationBuilder.UpdateData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 20,
                column: "DBTable",
                value: "FormData");
        }
    }
}
