using Domain.Common;
using Domain.Entities.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class FormDataConfiguration : IEntityTypeConfiguration<FormData>
    {
        public void Configure(EntityTypeBuilder<FormData> builder)
        {
            builder.HasData(
                new FormData
                {
                    Id = 1,
                    DBTable = "FormData",
                    DBColumn = "DBTable",
                    EditorType = EditorTypes.input.GetKeyName(),
                    Name = "Table",
                    Required = true,
                    SortOrder = 1,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "",
                    Deleted = false
                },
                new FormData
                {
                    Id = 2,
                    DBTable = "FormData",
                    DBColumn = "DBColumn",
                    EditorType = EditorTypes.input.GetKeyName(),
                    Name = "Type",
                    Required = true,
                    SortOrder = 2,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "",
                    Deleted = false
                },
                new FormData
                {
                    Id = 3,
                    DBTable = "FormData",
                    DBColumn = "Name",
                    EditorType = EditorTypes.input.GetKeyName(),
                    Name = "Name",
                    Required = true,
                    SortOrder = 3,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "",
                    Deleted = false
                },
                new FormData
                {
                    Id = 4,
                    DBTable = "FormData",
                    DBColumn = "DefaultValue",
                    EditorType = EditorTypes.input.GetKeyName(),
                    Name = "Default Value",
                    Required = false,
                    SortOrder = 4,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "",
                    Deleted = false
                },
                new FormData
                {
                    Id = 5,
                    DBTable = "FormData",
                    DBColumn = "Required",
                    EditorType = EditorTypes.checkbox.GetKeyName(),
                    Name = "Required",
                    Required = false,
                    SortOrder = 5,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "",
                    Deleted = false
                },
                new FormData
                {
                    Id = 6,
                    DBTable = "FormData",
                    DBColumn = "EditorType",
                    EditorType = EditorTypes.editor_type.GetKeyName(),
                    Name = "Type",
                    Required = true,
                    SortOrder = 6,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "This is the field type.",
                    Deleted = false
                },
                new FormData
                {
                    Id = 7,
                    DBTable = "FormData",
                    DBColumn = "MaxLength",
                    EditorType = EditorTypes.number.GetKeyName(),
                    Name = "Max Length",
                    Required = false,
                    SortOrder = 7,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "",
                    Deleted = false,
                    MinValue = 1,
                    MaxValue = 2000
                },
                new FormData
                {
                    Id = 8,
                    DBTable = "FormData",
                    DBColumn = "MinValue",
                    EditorType = EditorTypes.number.GetKeyName(),
                    Name = "Min Value",
                    Required = false,
                    SortOrder = 8,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "",
                    Deleted = false,
                    MinValue = 1,
                    MaxValue = 2000
                },
                new FormData
                {
                    Id = 9,
                    DBTable = "FormData",
                    DBColumn = "MaxValue",
                    EditorType = EditorTypes.number.GetKeyName(),
                    Name = "Max Value",
                    Required = false,
                    SortOrder = 9,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "",
                    Deleted = false,
                    MinValue = 1,
                    MaxValue = 2000
                },
                new FormData
                {
                    Id = 10,
                    DBTable = "FormData",
                    DBColumn = "SortOrder",
                    EditorType = EditorTypes.input.GetKeyName(),
                    Name = "Sort Order",
                    Required = false,
                    SortOrder = 10,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "",
                    Deleted = false
                },
                new FormData
                {
                    Id = 11,
                    DBTable = "FormData",
                    DBColumn = "Tooltip",
                    EditorType = EditorTypes.input.GetKeyName(),
                    Name = "Tooltip",
                    Required = false,
                    SortOrder = 11,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "",
                    Deleted = false
                },
                new FormData
                {
                    Id = 12,
                    DBTable = "FormData",
                    DBColumn = "Javascript",
                    EditorType = EditorTypes.input.GetKeyName(),
                    Name = "Javascript",
                    Required = false,
                    SortOrder = 12,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "",
                    Deleted = false
                },
                new FormData
                {
                    Id = 13,
                    DBTable = "FormData",
                    DBColumn = "SourceEndpoint",
                    EditorType = EditorTypes.input.GetKeyName(),
                    Name = "Source Endpoint",
                    Required = false,
                    SortOrder = 13,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "Endpoint to retrieve data for dropdown",
                    Deleted = false
                },
                new FormData
                {
                    Id = 14,
                    DBTable = "FormData",
                    DBColumn = "HttpMethod",
                    EditorType = EditorTypes.input.GetKeyName(),
                    Name = "HttpMethod",
                    Required = false,
                    SortOrder = 14,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "GET,POST",
                    Deleted = false
                },
                new FormData
                {
                    Id = 15,
                    DBTable = "FormData",
                    DBColumn = "DataText",
                    EditorType = EditorTypes.input.GetKeyName(),
                    Name = "Data Text",
                    Required = false,
                    SortOrder = 15,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "the text field will be displayed from data retrieved from endpoint",
                    Deleted = false
                },
                new FormData
                {
                    Id = 16,
                    DBTable = "FormData",
                    DBColumn = "DataValue",
                    EditorType = EditorTypes.input.GetKeyName(),
                    Name = "Data Value",
                    Required = false,
                    SortOrder = 16,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "the value field will be displayed from data retrieved from endpoint",
                    Deleted = false
                },
                new FormData
                {
                    Id = 17,
                    DBTable = "FormData",
                    DBColumn = "Width",
                    EditorType = EditorTypes.input.GetKeyName(),
                    Name = "Width",
                    Required = false,
                    SortOrder = 17,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "",
                    Deleted = false
                },
                new FormData
                {
                    Id = 18,
                    DBTable = "FormData",
                    DBColumn = "Height",
                    EditorType = EditorTypes.input.GetKeyName(),
                    Name = "Height",
                    Required = false,
                    SortOrder = 18,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "",
                    Deleted = false
                },
                new FormData
                {
                    Id = 19,
                    DBTable = "FormData",
                    DBColumn = "ClassCSS",
                    EditorType = EditorTypes.input.GetKeyName(),
                    Name = "Class CSS",
                    Required = false,
                    SortOrder = 19,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "css class will be applied for this editor",
                    Deleted = false
                },
                new FormData
                {
                    Id = 20,
                    DBTable = "FormData",
                    DBColumn = "PermissionRoles",
                    EditorType = EditorTypes.input.GetKeyName(),
                    Name = "Permission Roles",
                    Required = false,
                    SortOrder = 20,
                    MaxLength = 500,
                    PermissionRoles = "Administrator",
                    Tooltip = "roles are seperated by comma",
                    Deleted = false
                }
                );
        }
    }
}
