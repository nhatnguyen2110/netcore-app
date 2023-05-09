using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Models;
using Application.Models.Form;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Functions.FormDatas.Queries.FormFieldList
{
	public class FormFieldListQuery : RequestParameter, IRequest<Response<PaginatedList<FormFieldDto>>>
	{
		public string? DBTable { get; set; }
		public override string OrderBy { get; set; } = "SortOrder";
		public string requestId { get; set; } = Guid.NewGuid().ToString();
	}
	public class FormFieldListQueryHandler : BaseHandler<FormFieldListQuery, Response<PaginatedList<FormFieldDto>>>
	{
		public FormFieldListQueryHandler(ICommonService commonService,
			ILogger<FormFieldListQuery> logger) : base(commonService, logger)
		{
		}
		public async override Task<Response<PaginatedList<FormFieldDto>>> Handle(FormFieldListQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _commonService.ApplicationDBContext.FormDatas
					.AsNoTracking()
					.Where(x => !x.Deleted)
					.Where(x =>
						x.DBTable == request.DBTable
					)
					.OrderBy(x => x.SortOrder)
					.ProjectTo<FormFieldDto>(this._commonService.Mapper?.ConfigurationProvider)
					.PaginatedListAsync(request.PageNumber, request.PageSize);
				return Response<PaginatedList<FormFieldDto>>.Success(result, request.requestId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to load. Request: {Name} {@Request}", typeof(FormFieldListQuery).Name, request);
				return new Response<PaginatedList<FormFieldDto>>(false, ex.Message, ex.Message, "Failed to load", request.requestId);
			}
		}
	}
}
