using Domain.Common;
using Domain.Enums;

namespace Domain.Entities.Webhook
{
    public class WebHook : DomainEntity<Guid>, IAuditableEntity
    {
        public WebHook()
        {
            this.Headers = new HashSet<WebHookHeader>();
            this.HookEventTypes = new List<string>();
            this.Records = new List<WebHookRecord>();
        }

        /// <summary>
        /// Webhook endpoint
        /// </summary>
        public string? WebHookUrl { get; set; }

        /// <summary>
        /// Webhook secret
        /// </summary>
        public string? Secret { get; set; }

        /// <summary>
        /// Content Type
        /// </summary>
        public string? ContentType { get; set; }

        /// <summary>
        /// Is active / NotActiv
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Hook Events context
        /// EF Core don't support store List of Enums so we store as a list of string
        /// </summary>
        public List<string> HookEventTypes { get; set; }

        /// <summary>
        /// Additional HTTP headers. Will be sent with hook.
        /// </summary>
        virtual public HashSet<WebHookHeader> Headers { get; set; }

        /// <summary>
        /// Hook call records history
        /// </summary>
        virtual public ICollection<WebHookRecord> Records { get; set; }

        /// <summary>
        /// Timestamp of last hook trigger
        /// </summary>
        /// <value></value>
        public DateTimeOffset? LastTriggerAtUTC { get; set; }
        public bool Deleted { get; set; }
        public DateTimeOffset? CreatedAtUTC { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? LastModifiedAtUTC { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
