using Application.Common.Mappings;
using Domain.Entities.Webhook;
using Domain.Enums;

namespace Application.Models.WebHooks
{
    public class WebHookRecordDto : IMapFrom<WebHookRecord>
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Linked Webhook Id
        /// </summary>
        public Guid WebHookID { get; set; }

        /// <summary>
        /// WebHookType
        /// </summary>
        public HookEventType HookEventType { get; set; }

        /// <summary>
        /// Hook result enum
        /// </summary>
        public RecordResult Result { get; set; }

        /// <summary>
        /// Response
        /// </summary>
        public string? StatusCode { get; set; }

        /// <summary>
        /// Response json
        /// </summary>
        public string? ResponseBody { get; set; }

        /// <summary>
        /// Request json
        /// </summary>
        public string? RequestBody { get; set; }

        /// <summary>
        /// Request Headers json
        /// </summary>
        public string? RequestHeaders { get; set; }

        /// <summary>
        /// Exception
        /// </summary>
        public string? Exception { get; set; }

        /// <summary>
        /// Hook Call Timestamp
        /// </summary>
        public DateTimeOffset RunAtUTC { get; set; }
    }
}
