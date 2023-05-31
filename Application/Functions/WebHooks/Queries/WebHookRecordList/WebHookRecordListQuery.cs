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

namespace Application.Functions.WebHooks.Queries.WebHookRecordList
{
    public class WebHookRecordListQuery : RequestParameter, IRequest<Response<PaginatedList<WebHookRecordDto>>>
    {
        public Guid WebHookId { get; set; }
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class WebHookRecordListQueryHandler : BaseHandler<WebHookRecordListQuery, Response<PaginatedList<WebHookRecordDto>>>
    {
        public WebHookRecordListQueryHandler(ICommonService commonService,
            ILogger<WebHookRecordListQuery> logger) : base(commonService, logger)
        {
        }
        public async override Task<Response<PaginatedList<WebHookRecordDto>>> Handle(WebHookRecordListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _commonService.ApplicationDBContext.WebHookRecords
                    .AsNoTracking()
                    .Where(x => x.WebHookID == request.WebHookId)
                    .OrderBy(request.OrderBy)
                    .ProjectTo<WebHookRecordDto>(_commonService.Mapper?.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
                return Response<PaginatedList<WebHookRecordDto>>.Success(result, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load. Request: {Name} {@Request}", typeof(WebHookRecordListQuery).Name, request);
                return new Response<PaginatedList<WebHookRecordDto>>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to load", request.requestId);
            }
        }
    }
}
