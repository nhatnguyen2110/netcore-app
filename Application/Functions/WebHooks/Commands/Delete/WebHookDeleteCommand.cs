using Application.Common.Interfaces;
using Application.Models;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Functions.WebHooks.Commands.Delete
{
    public class WebHookDeleteCommand : IRequest<Response<Unit>>
    {
        public Guid Id { get; set; }
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class WebHookDeleteCommandHandler : BaseHandler<WebHookDeleteCommand, Response<Unit>>
    {
        public WebHookDeleteCommandHandler(ICommonService commonService,
            ILogger<WebHookDeleteCommand> logger) : base(commonService, logger)
        {
        }
        public async override Task<Response<Unit>> Handle(WebHookDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _commonService.ApplicationDBContext.WebHooks.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (entity == null)
                {
                    throw new Exception("Cannot find item");
                }
                entity.Deleted = true;

                await _commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
                return Response<Unit>.Success(Unit.Value, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete. Request: {Name} {@Request}", typeof(WebHookDeleteCommand).Name, request);
                return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to delete", request.requestId);
            }
        }
    }
}
