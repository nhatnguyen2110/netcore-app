﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230509090025_UpdateFormDataValues")]
    partial class UpdateFormDataValues
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Application.Common.DBSupports.SPSingleValueQueryResultString", b =>
                {
                    b.Property<string>("val")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("GetSingleValueQueryString");
                });

            modelBuilder.Entity("Domain.Entities.Forms.FormData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClassCSS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("CreatedAtUTC")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DBColumn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DBTable")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DataText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DataValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DefaultValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("EditorType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Height")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HttpMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Javascript")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastModifiedAtUTC")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaxLength")
                        .HasColumnType("int");

                    b.Property<int?>("MaxValue")
                        .HasColumnType("int");

                    b.Property<int?>("MinValue")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PermissionRoles")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Required")
                        .HasColumnType("bit");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<string>("SourceEndpoint")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tooltip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Width")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FormDatas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DBColumn = "DBTable",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "INPUT",
                            MaxLength = 500,
                            Name = "Table",
                            PermissionRoles = "Administrator",
                            Required = true,
                            SortOrder = 1,
                            Tooltip = ""
                        },
                        new
                        {
                            Id = 2,
                            DBColumn = "DBColumn",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "INPUT",
                            MaxLength = 500,
                            Name = "Type",
                            PermissionRoles = "Administrator",
                            Required = true,
                            SortOrder = 2,
                            Tooltip = ""
                        },
                        new
                        {
                            Id = 3,
                            DBColumn = "Name",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "INPUT",
                            MaxLength = 500,
                            Name = "Name",
                            PermissionRoles = "Administrator",
                            Required = true,
                            SortOrder = 3,
                            Tooltip = ""
                        },
                        new
                        {
                            Id = 4,
                            DBColumn = "DefaultValue",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "INPUT",
                            MaxLength = 500,
                            Name = "Default Value",
                            PermissionRoles = "Administrator",
                            Required = false,
                            SortOrder = 4,
                            Tooltip = ""
                        },
                        new
                        {
                            Id = 5,
                            DBColumn = "Required",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "CHECKBOX",
                            MaxLength = 500,
                            Name = "Required",
                            PermissionRoles = "Administrator",
                            Required = false,
                            SortOrder = 5,
                            Tooltip = ""
                        },
                        new
                        {
                            Id = 6,
                            DBColumn = "EditorType",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "EDITOR_TYPE",
                            MaxLength = 500,
                            Name = "Type",
                            PermissionRoles = "Administrator",
                            Required = true,
                            SortOrder = 6,
                            Tooltip = "This is the field type."
                        },
                        new
                        {
                            Id = 7,
                            DBColumn = "MaxLength",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "NUMBER",
                            MaxLength = 500,
                            MaxValue = 2000,
                            MinValue = 1,
                            Name = "Max Length",
                            PermissionRoles = "Administrator",
                            Required = false,
                            SortOrder = 7,
                            Tooltip = ""
                        },
                        new
                        {
                            Id = 8,
                            DBColumn = "MinValue",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "NUMBER",
                            MaxLength = 500,
                            MaxValue = 2000,
                            MinValue = 1,
                            Name = "Min Value",
                            PermissionRoles = "Administrator",
                            Required = false,
                            SortOrder = 8,
                            Tooltip = ""
                        },
                        new
                        {
                            Id = 9,
                            DBColumn = "MaxValue",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "NUMBER",
                            MaxLength = 500,
                            MaxValue = 2000,
                            MinValue = 1,
                            Name = "Max Value",
                            PermissionRoles = "Administrator",
                            Required = false,
                            SortOrder = 9,
                            Tooltip = ""
                        },
                        new
                        {
                            Id = 10,
                            DBColumn = "SortOrder",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "INPUT",
                            MaxLength = 500,
                            Name = "Sort Order",
                            PermissionRoles = "Administrator",
                            Required = false,
                            SortOrder = 10,
                            Tooltip = ""
                        },
                        new
                        {
                            Id = 11,
                            DBColumn = "Tooltip",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "INPUT",
                            MaxLength = 500,
                            Name = "Tooltip",
                            PermissionRoles = "Administrator",
                            Required = false,
                            SortOrder = 11,
                            Tooltip = ""
                        },
                        new
                        {
                            Id = 12,
                            DBColumn = "Javascript",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "INPUT",
                            MaxLength = 500,
                            Name = "Javascript",
                            PermissionRoles = "Administrator",
                            Required = false,
                            SortOrder = 12,
                            Tooltip = ""
                        },
                        new
                        {
                            Id = 13,
                            DBColumn = "SourceEndpoint",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "INPUT",
                            MaxLength = 500,
                            Name = "Source Endpoint",
                            PermissionRoles = "Administrator",
                            Required = false,
                            SortOrder = 13,
                            Tooltip = "Endpoint to retrieve data for dropdown"
                        },
                        new
                        {
                            Id = 14,
                            DBColumn = "HttpMethod",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "INPUT",
                            MaxLength = 500,
                            Name = "HttpMethod",
                            PermissionRoles = "Administrator",
                            Required = false,
                            SortOrder = 14,
                            Tooltip = "GET,POST"
                        },
                        new
                        {
                            Id = 15,
                            DBColumn = "DataText",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "INPUT",
                            MaxLength = 500,
                            Name = "Data Text",
                            PermissionRoles = "Administrator",
                            Required = false,
                            SortOrder = 15,
                            Tooltip = "the text field will be displayed from data retrieved from endpoint"
                        },
                        new
                        {
                            Id = 16,
                            DBColumn = "DataValue",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "INPUT",
                            MaxLength = 500,
                            Name = "Data Value",
                            PermissionRoles = "Administrator",
                            Required = false,
                            SortOrder = 16,
                            Tooltip = "the value field will be displayed from data retrieved from endpoint"
                        },
                        new
                        {
                            Id = 17,
                            DBColumn = "Width",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "INPUT",
                            MaxLength = 500,
                            Name = "Width",
                            PermissionRoles = "Administrator",
                            Required = false,
                            SortOrder = 17,
                            Tooltip = ""
                        },
                        new
                        {
                            Id = 18,
                            DBColumn = "Height",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "INPUT",
                            MaxLength = 500,
                            Name = "Height",
                            PermissionRoles = "Administrator",
                            Required = false,
                            SortOrder = 18,
                            Tooltip = ""
                        },
                        new
                        {
                            Id = 19,
                            DBColumn = "ClassCSS",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "INPUT",
                            MaxLength = 500,
                            Name = "Class CSS",
                            PermissionRoles = "Administrator",
                            Required = false,
                            SortOrder = 19,
                            Tooltip = "css class will be applied for this editor"
                        },
                        new
                        {
                            Id = 20,
                            DBColumn = "PermissionRoles",
                            DBTable = "FormDatas",
                            Deleted = false,
                            EditorType = "INPUT",
                            MaxLength = 500,
                            Name = "Permission Roles",
                            PermissionRoles = "Administrator",
                            Required = false,
                            SortOrder = 20,
                            Tooltip = "roles are seperated by comma"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Log.AuditLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Action")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Exception")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logger")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("Domain.Entities.User.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "f0967a26-d5ba-41a8-92e5-747783c4cc07",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "052997d8-20f2-4089-b5e1-50b296461d51",
                            Name = "Member",
                            NormalizedName = "MEMBER"
                        },
                        new
                        {
                            Id = "d681023c-1441-4134-8253-f660cf26a716",
                            Name = "Guest",
                            NormalizedName = "GUEST"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Domain.Entities.User.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Domain.Entities.User.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Domain.Entities.User.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
