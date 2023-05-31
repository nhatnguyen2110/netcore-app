using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Models;
using Application.Models.WebHooks;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;

namespace Application.Functions.WebHooks.Queries.WebHookList
{
    public class WebHookListQuery : RequestParameter, IRequest<Response<PaginatedList<WebHookDto>>>
    {
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class WebHookListQueryHandler : BaseHandler<WebHookListQuery, Response<PaginatedList<WebHookDto>>>
    {
        public WebHookListQueryHandler(ICommonService commonService,
            ILogger<WebHookListQuery> logger) : base(commonService, logger)
        {
        }
        public async override Task<Response<PaginatedList<WebHookDto>>> Handle(WebHookListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _commonService.ApplicationDBContext.WebHooks
                    .AsNoTracking()
                    .Where(x => !x.Deleted)
                    .OrderBy(request.OrderBy)
                    .ProjectTo<WebHookDto>(this._commonService.Mapper?.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
                return Response<PaginatedList<WebHookDto>>.Success(result, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load. Request: {Name} {@Request}", typeof(WebHookListQuery).Name, request);
                return new Response<PaginatedList<WebHookDto>>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to load", request.requestId);
            }
        }
    }
}
