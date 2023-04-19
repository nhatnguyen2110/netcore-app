namespace Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime CreatedAtUTC { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModifiedAtUTC { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
