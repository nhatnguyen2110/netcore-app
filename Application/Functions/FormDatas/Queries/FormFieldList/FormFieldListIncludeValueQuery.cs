using Application.Common.Interfaces;
using Application.Models;
using Application.Models.Form;
using Domain;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Functions.FormDatas.Queries.FormFieldList
{
	public class FormFieldListIncludeValueQuery : IRequest<Response<List<FormFieldDto>>>
	{
		public int ParentId { get; set; }
		public string? DBTable { get; set; }
		public string requestId { get; set; } = Guid.NewGuid().ToString();
	}
	public class FormFieldListIncludeValueQueryHandler : BaseHandler<FormFieldListIncludeValueQuery, Response<List<FormFieldDto>>>
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly IIdentityService _identityService;
		public FormFieldListIncludeValueQueryHandler(ICommonService commonService,
			ICurrentUserService currentUserService,
			IIdentityService identityService,
			ILogger<FormFieldListIncludeValueQuery> logger) : base(commonService, logger)
		{
			_currentUserService = currentUserService;
			_identityService = identityService;
		}
		public async override Task<Response<List<FormFieldDto>>> Handle(FormFieldListIncludeValueQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var result = new List<FormFieldDto>();
				var fields = await _commonService.ApplicationDBContext.FormDatas
					.AsNoTracking()
					.Where(x => !x.Deleted)
					.Where(x =>
						x.DBTable == request.DBTable
					)
					.OrderBy(x => x.SortOrder).ToListAsync(cancellationToken);
				foreach ( var field in fields ) {
					// check permissions
					if (String.IsNullOrEmpty(field.PermissionRoles) || await IsCurrentUserInRoles(field.PermissionRoles))
					{
						var fieldDto = _commonService.Mapper.Map<FormFieldDto>(field);
						if (request.ParentId > 0)
						{
							// retrieve value
							var param = new SqlParameter[]
									{
										new SqlParameter() {
											ParameterName = "@par",
											Value = request.ParentId
										}
									};
							var editVal = await _commonService.ApplicationDBContext.GetSingleValueQueryString
								.FromSqlRaw("SELECT CAST([" + field.DBColumn + "] AS nvarchar(MAX)) as val FROM dbo.[" + field.DBTable + "] WHERE id=@par", param).ToListAsync(cancellationToken);
							fieldDto.DBValue = (editVal == null || editVal.Count() == 0 ? "" : editVal[0].val);
						}
						result.Add(fieldDto);
					}
				}
				return Response<List<FormFieldDto>>.Success(result, request.requestId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to load. Request: {Name} {@Request}", typeof(FormFieldListIncludeValueQuery).Name, request);
				return new Response<List<FormFieldDto>>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to load", request.requestId);
			}
		}
		private async Task<bool> IsCurrentUserInRoles(string roles)
		{
			var rolesList = roles.Split(',');
			foreach (var role in rolesList)
			{
#pragma warning disable CS8604 // Possible null reference argument.
				if (await _identityService.IsInRoleAsync(_currentUserService.UserId, role))
				{
					return true;
				}
#pragma warning restore CS8604 // Possible null reference argument.
			}
			return false;
		}
	}
}
