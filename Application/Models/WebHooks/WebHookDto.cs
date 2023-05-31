using Application.Common.Mappings;
using Domain.Entities.Webhook;

namespace Application.Models.WebHooks
{
    public class WebHookDto : IMapFrom<WebHook>
    {
        public Guid Id { get; set; }
        public string? WebHookUrl { get; set; }
        public string? Secret { get; set; }
        public string? ContentType { get; set; }
        public bool IsActive { get; set; }
        public List<string> HookEventTypes { get; set; } = new List<string>();
        public DateTimeOffset? LastTriggerAtUTC { get; set; }
        public DateTimeOffset? CreatedAtUTC { get; set; }
        public HashSet<WebHookHeaderDto> Headers { get; set; } = new HashSet<WebHookHeaderDto>();

    }
}
