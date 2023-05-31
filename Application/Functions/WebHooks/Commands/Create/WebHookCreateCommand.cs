using Application.Common.Interfaces;
using Application.Models;
using Application.Models.WebHooks;
using Domain;
using Domain.Entities.Webhook;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Functions.WebHooks.Commands.Create
{
    public class WebHookCreateCommand : IRequest<Response<string>>
    {
        public string? WebHookUrl { get; set; }
        public string? Secret { get; set; }
        public string? ContentType { get; set; }
        public bool IsActive { get; set; }
        public List<string> HookEventTypes { get; set; } = new List<string>();
        public HashSet<WebHookHeaderDto> Headers { get; set; } = new HashSet<WebHookHeaderDto>();
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class WebHookCreateCommandHandler : BaseHandler<WebHookCreateCommand, Response<string>>
    {
        public WebHookCreateCommandHandler(ICommonService commonService,
            ILogger<WebHookCreateCommand> logger) : base(commonService, logger)
        {
        }
        public async override Task<Response<string>> Handle(WebHookCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var headers = new HashSet<WebHookHeader>();
                foreach (var item in request.Headers)
                {
                    headers.Add(new WebHookHeader
                    {
                        Name = item.Name,
                        Value = item.Value,
                        CreatedAtUTC = DateTimeOffset.UtcNow
                    });
                }
                var entity = new WebHook
                {
                    Id = Guid.NewGuid(),
                    WebHookUrl= request.WebHookUrl,
                    Secret= request.Secret,
                    ContentType = request.ContentType,
                    IsActive = request.IsActive,
                    HookEventTypes = request.HookEventTypes,
                    Deleted = false,
                    Headers = headers
                };
                _commonService.ApplicationDBContext.WebHooks.Add(entity);
                await _commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
                return Response<string>.Success(entity.Id.ToString(), request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create. Request: {Name} {@Request}", typeof(WebHookCreateCommand).Name, request);
                return new Response<string>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to create", request.requestId);
            }
        }
    }
}
