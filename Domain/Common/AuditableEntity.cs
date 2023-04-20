namespace Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTimeOffset? CreatedAtUTC { get; set; }

        public string? CreatedBy { get; set; }

        public DateTimeOffset? LastModifiedAtUTC { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
