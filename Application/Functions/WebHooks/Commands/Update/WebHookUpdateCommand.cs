using Application.Common.Interfaces;
using Application.Functions.WebHooks.Commands.Create;
using Application.Models;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Functions.WebHooks.Commands.Update
{
    public class WebHookUpdateCommand : IRequest<Response<Unit>>
    {
        public Guid Id { get; set; }
        public string? WebHookUrl { get; set; }
        public string? Secret { get; set; }
        public string? ContentType { get; set; }
        public bool IsActive { get; set; }
        public List<string> HookEventTypes { get; set; } = new List<string>();
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class WebHookUpdateCommandHandler : BaseHandler<WebHookUpdateCommand, Response<Unit>>
    {
        public WebHookUpdateCommandHandler(ICommonService commonService,
            ILogger<WebHookUpdateCommand> logger) : base(commonService, logger)
        {
        }
        public async override Task<Response<Unit>> Handle(WebHookUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _commonService.ApplicationDBContext.WebHooks.FirstOrDefaultAsync(x => x.Id == request.Id && !x.Deleted);
                if (entity == null)
                {
                    throw new Exception("Cannot find item");
                }
                entity.WebHookUrl = request.WebHookUrl;
                entity.Secret = request.Secret;
                entity.ContentType = request.ContentType;
                entity.IsActive= request.IsActive;
                entity.HookEventTypes = request.HookEventTypes;

                await _commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
                return Response<Unit>.Success(Unit.Value, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update. Request: {Name} {@Request}", typeof(WebHookCreateCommand).Name, request);
                return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to update", request.requestId);
            }
        }
    }
}
