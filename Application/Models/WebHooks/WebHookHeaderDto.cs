using Application.Common.Mappings;
using Domain.Entities.Webhook;

namespace Application.Models.WebHooks
{
    public class WebHookHeaderDto : IMapFrom<WebHookHeader>
    {
        public int Id { get; set; }
        public Guid WebHookID { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
}
