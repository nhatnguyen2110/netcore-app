using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DBTable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DBColumn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditorType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    MaxLength = table.Column<int>(type: "int", nullable: true),
                    MinValue = table.Column<int>(type: "int", nullable: true),
                    MaxValue = table.Column<int>(type: "int", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    Tooltip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Javascript = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HttpMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassCSS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermissionRoles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUTC = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAtUTC = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c24e743-442d-43cc-85b7-5d7a5f69ee40", null, "Administrator", "ADMINISTRATOR" },
                    { "ae41d58a-fde1-4516-a52a-2967b1889d90", null, "Guest", "GUEST" },
                    { "e44eba1c-3614-4191-882e-d8c6b15cd802", null, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "FormDatas",
                columns: new[] { "Id", "ClassCSS", "CreatedAtUTC", "CreatedBy", "DBColumn", "DBTable", "DataText", "DataValue", "DefaultValue", "Deleted", "EditorType", "Height", "HttpMethod", "Javascript", "LastModifiedAtUTC", "LastModifiedBy", "MaxLength", "MaxValue", "MinValue", "Name", "PermissionRoles", "Required", "SortOrder", "SourceEndpoint", "Tooltip", "Width" },
                values: new object[,]
                {
                    { 1, null, null, null, "DBTable", "FormData", null, null, null, false, "INPUT", null, null, null, null, null, 500, null, null, "Table", "Administrator", true, 1, null, "", null },
                    { 2, null, null, null, "DBColumn", "FormData", null, null, null, false, "INPUT", null, null, null, null, null, 500, null, null, "Type", "Administrator", true, 2, null, "", null },
                    { 3, null, null, null, "Name", "FormData", null, null, null, false, "INPUT", null, null, null, null, null, 500, null, null, "Name", "Administrator", true, 3, null, "", null },
                    { 4, null, null, null, "DefaultValue", "FormData", null, null, null, false, "INPUT", null, null, null, null, null, 500, null, null, "Default Value", "Administrator", false, 4, null, "", null },
                    { 5, null, null, null, "Required", "FormData", null, null, null, false, "CHECKBOX", null, null, null, null, null, 500, null, null, "Required", "Administrator", false, 5, null, "", null },
                    { 6, null, null, null, "EditorType", "FormData", null, null, null, false, "EDITOR_TYPE", null, null, null, null, null, 500, null, null, "Type", "Administrator", true, 6, null, "This is the field type.", null },
                    { 7, null, null, null, "MaxLength", "FormData", null, null, null, false, "NUMBER", null, null, null, null, null, 500, 2000, 1, "Max Length", "Administrator", false, 7, null, "", null },
                    { 8, null, null, null, "MinValue", "FormData", null, null, null, false, "NUMBER", null, null, null, null, null, 500, 2000, 1, "Min Value", "Administrator", false, 8, null, "", null },
                    { 9, null, null, null, "MaxValue", "FormData", null, null, null, false, "NUMBER", null, null, null, null, null, 500, 2000, 1, "Max Value", "Administrator", false, 9, null, "", null },
                    { 10, null, null, null, "SortOrder", "FormData", null, null, null, false, "INPUT", null, null, null, null, null, 500, null, null, "Sort Order", "Administrator", false, 10, null, "", null },
                    { 11, null, null, null, "Tooltip", "FormData", null, null, null, false, "INPUT", null, null, null, null, null, 500, null, null, "Tooltip", "Administrator", false, 11, null, "", null },
                    { 12, null, null, null, "Javascript", "FormData", null, null, null, false, "INPUT", null, null, null, null, null, 500, null, null, "Javascript", "Administrator", false, 12, null, "", null },
                    { 13, null, null, null, "SourceEndpoint", "FormData", null, null, null, false, "INPUT", null, null, null, null, null, 500, null, null, "Source Endpoint", "Administrator", false, 13, null, "Endpoint to retrieve data for dropdown", null },
                    { 14, null, null, null, "HttpMethod", "FormData", null, null, null, false, "INPUT", null, null, null, null, null, 500, null, null, "HttpMethod", "Administrator", false, 14, null, "GET,POST", null },
                    { 15, null, null, null, "DataText", "FormData", null, null, null, false, "INPUT", null, null, null, null, null, 500, null, null, "Data Text", "Administrator", false, 15, null, "the text field will be displayed from data retrieved from endpoint", null },
                    { 16, null, null, null, "DataValue", "FormData", null, null, null, false, "INPUT", null, null, null, null, null, 500, null, null, "Data Value", "Administrator", false, 16, null, "the value field will be displayed from data retrieved from endpoint", null },
                    { 17, null, null, null, "Width", "FormData", null, null, null, false, "INPUT", null, null, null, null, null, 500, null, null, "Width", "Administrator", false, 17, null, "", null },
                    { 18, null, null, null, "Height", "FormData", null, null, null, false, "INPUT", null, null, null, null, null, 500, null, null, "Height", "Administrator", false, 18, null, "", null },
                    { 19, null, null, null, "ClassCSS", "FormData", null, null, null, false, "INPUT", null, null, null, null, null, 500, null, null, "Class CSS", "Administrator", false, 19, null, "css class will be applied for this editor", null },
                    { 20, null, null, null, "PermissionRoles", "FormData", null, null, null, false, "INPUT", null, null, null, null, null, 500, null, null, "Permission Roles", "Administrator", false, 20, null, "roles are seperated by comma", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FormDatas");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
