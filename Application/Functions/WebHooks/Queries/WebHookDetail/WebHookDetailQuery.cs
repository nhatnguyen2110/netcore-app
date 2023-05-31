using Application.Common.Interfaces;
using Application.Models;
using Application.Models.WebHooks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Functions.WebHooks.Queries.WebHookDetail
{
    public class WebHookDetailQuery : IRequest<Response<WebHookDto>>
    {
        public Guid Id { get; set; }
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class WebHookDetailQueryHandler : BaseHandler<WebHookDetailQuery, Response<WebHookDto>>
    {
        public WebHookDetailQueryHandler(ICommonService commonService,
            ILogger<WebHookDetailQuery> logger) : base(commonService, logger)
        {
        }
        public async override Task<Response<WebHookDto>> Handle(WebHookDetailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _commonService.ApplicationDBContext.WebHooks.Include(x => x.Headers).FirstOrDefaultAsync(x => x.Id == request.Id && !x.Deleted, cancellationToken);
                if (entity == null)
                {
                    throw new Exception("Cannot find item");
                }
                var result = _commonService.Mapper.Map<WebHookDto>(entity);
                return Response<WebHookDto>.Success(result, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load. Request: {Name} {@Request}", typeof(WebHookDetailQuery).Name, request);
                return new Response<WebHookDto>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to load", request.requestId);
            }
        }
    }
}
