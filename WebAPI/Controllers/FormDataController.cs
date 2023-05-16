using Application.Functions.FormDatas.Commands.FormBuilder;
using Application.Functions.FormDatas.Queries.FormFieldList;
using Application.Functions.FormDatas.Queries.FormList;
using Application.Models;
using Application.Models.Form;
using MediatR;
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
		[Authorize]
		[HttpPost("submit-insert/{tableName}")]
		public async Task<ActionResult<Response<string>>> SubmitInsert(string tableName)
		{
			var result = await Mediator.Send(new FormBuilderInsertCommand
			{
				TableName = tableName,
				Fields = Request.Form.Keys.ToDictionary(k => k, v => Request.Form[v].ToString())
			});
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
        [HttpPost("submit-update/{tableName}/{parentId}")]
        public async Task<ActionResult<Response<Unit>>> SubmitUpdate(string tableName, string parentId)
        {
            var result = await Mediator.Send(new FormBuilderUpdateCommand
            {
                TableName = tableName,
				ParentColumnName= "Id", // primary key for almost tables
				ParentId = parentId,
                Fields = Request.Form.Keys.ToDictionary(k => k, v => Request.Form[v].ToString())
            });
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
