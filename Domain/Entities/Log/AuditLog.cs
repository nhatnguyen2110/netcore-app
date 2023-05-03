using Domain.Common;

namespace Domain.Entities.Log
{
    public class AuditLog : DomainEntity<int>
    {
        public DateTime CreatedOnUtc { get; set; }
        public string? Level { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
        public string? StackTrace { get; set; }
        public string? Action { get; set; }
        public string? Logger { get; set; }
        public string? Url { get; set; }
    }
}
