using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Models;
using Application.Models.Form;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;

namespace Application.Functions.FormDatas.Queries.FormList
{
	public class FormListQuery : RequestParameter,IRequest<Response<PaginatedList<FormDBTableDto>>>
	{
		public string? Keyword { get; set; }
		public override string OrderBy { get; set; } = "DBTable";
		public string requestId { get; set; } = Guid.NewGuid().ToString();
	}
	public class FormDataListQueryHandler : BaseHandler<FormListQuery, Response<PaginatedList<FormDBTableDto>>>
	{
		public FormDataListQueryHandler(ICommonService commonService,
			ILogger<FormListQuery> logger) : base(commonService, logger)
		{
		}
		public async override Task<Response<PaginatedList<FormDBTableDto>>> Handle(FormListQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _commonService.ApplicationDBContext.FormDatas
					.AsNoTracking()
					.Where(x => !x.Deleted)
					.Where(x =>
					string.IsNullOrEmpty(request.Keyword)
					|| (x.DBTable + "").Contains(request.Keyword)
					)
					.OrderBy(request.OrderBy)
					.GroupBy(g => new { g.DBTable })
					.Select(s => new FormDBTableDto
					{
						DBTable = s.Key.DBTable,
						Count = s.Count()
					})
					.PaginatedListAsync(request.PageNumber, request.PageSize);
				return Response<PaginatedList<FormDBTableDto>>.Success(result, request.requestId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to load. Request: {Name} {@Request}", typeof(FormListQuery).Name, request);
				return new Response<PaginatedList<FormDBTableDto>>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to load", request.requestId);
			}
		}
	}
}
