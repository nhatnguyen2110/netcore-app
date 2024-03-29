﻿using Domain.Common;

namespace Domain.Entities.Forms
{
    public class FormData : DomainEntity<int>, IAuditableEntity
    {
        public string? DBTable { get; set; }
        public string? DBColumn { get; set; }
        public string? EditorType { get; set; }
        public string? Name { get; set; }
        public string? DefaultValue { get; set; }
        public bool Required { get; set; }
        public int? MaxLength { get; set; }
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public int SortOrder { get; set; }
        public string? Tooltip { get; set; }
        public string? Javascript { get; set; }
        public string? SourceEndpoint { get; set; }
        public string? HttpMethod { get; set; }
        public string? DataText { get; set; }
        public string? DataValue { get; set; }
        public string? Width { get; set; }
        public string? Height { get; set; }
        public string? ClassCSS { get; set; }
        public string? PermissionRoles { get; set; }
        public bool Deleted { get; set; }
        public DateTimeOffset? CreatedAtUTC { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? LastModifiedAtUTC { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
