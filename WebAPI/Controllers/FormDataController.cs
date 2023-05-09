using Application.Functions.FormDatas.Queries.FormFieldList;
using Application.Functions.FormDatas.Queries.FormList;
using Application.Models;
using Application.Models.Form;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Common;

namespace WebAPI.Controllers
{
	public class FormDataController : ApiControllerBase
	{
		[Authorize(Roles = "Administrator")]
		[HttpGet("[action]")]
		public async Task<ActionResult<Response<PaginatedList<FormDBTableDto>>>> GetFormList([FromQuery] FormListQuery query)
		{
			var result = await Mediator.Send(query);
			if (result.Succeeded)
			{
				return result;
			}
			else
			{
				return BadRequest(result);
			}
		}
		[Authorize(Roles = "Administrator")]
		[HttpGet("[action]")]
		public async Task<ActionResult<Response<PaginatedList<FormFieldDto>>>> GetFormFieldList([FromQuery] FormFieldListQuery query)
		{
			var result = await Mediator.Send(query);
			if (result.Succeeded)
			{
				return result;
			}
			else
			{
				return BadRequest(result);
			}
		}
		[Authorize]
		[HttpGet("[action]")]
		public async Task<ActionResult<Response<List<FormFieldDto>>>> GetFormFieldIncludeValueList([FromQuery] FormFieldListIncludeValueQuery query)
		{
			var result = await Mediator.Send(query);
			if (result.Succeeded)
			{
				return result;
			}
			else
			{
				return BadRequest(result);
			}
		}
	}
}
