using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Delete_ColumnFormDatas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.InsertData(
                table: "FormDatas",
                columns: new[] { "Id", "ClassCSS", "CreatedAtUTC", "CreatedBy", "DBColumn", "DBTable", "DataText", "DataValue", "DefaultValue", "Deleted", "EditorType", "Height", "HttpMethod", "Javascript", "LastModifiedAtUTC", "LastModifiedBy", "MaxLength", "MaxValue", "MinValue", "Name", "PermissionRoles", "Required", "SortOrder", "SourceEndpoint", "Tooltip", "Width" },
                values: new object[] { 21, null, null, null, "Deleted", "FormDatas", null, null, "0", false, "CHECKBOX", null, null, null, null, null, null, null, null, "Deleted", "Administrator", true, 21, null, "set true as deleted", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FormDatas",
                keyColumn: "Id",
                keyValue: 21);
        }
    }
}
