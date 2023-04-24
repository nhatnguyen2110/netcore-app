namespace Domain.Common
{
    public interface IAuditableEntity
    {
        DateTimeOffset? CreatedAtUTC { get; set; }

        string? CreatedBy { get; set; }

        DateTimeOffset? LastModifiedAtUTC { get; set; }

        string? LastModifiedBy { get; set; }
    }
}
