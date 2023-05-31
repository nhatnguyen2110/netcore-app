using Application.Functions.WebHooks.Commands.Create;
using Application.Functions.WebHooks.Commands.Delete;
using Application.Functions.WebHooks.Commands.Update;
using Application.Functions.WebHooks.Queries.WebHookDetail;
using Application.Functions.WebHooks.Queries.WebHookList;
using Application.Functions.WebHooks.Queries.WebHookRecordList;
using Application.Models;
using Application.Models.WebHooks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Common;

namespace WebAPI.Controllers
{
    public class WebHookController : ApiControllerBase
    {
        [Authorize]
        [HttpGet("[action]")]
        public async Task<ActionResult<Response<PaginatedList<WebHookDto>>>> List([FromQuery] WebHookListQuery query, CancellationToken token)
        {
            var result = await Mediator.Send(query, token);
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
        public async Task<ActionResult<Response<PaginatedList<WebHookRecordDto>>>> Records([FromQuery] WebHookRecordListQuery query, CancellationToken token)
        {
            var result = await Mediator.Send(query, token);
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
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Response<WebHookDto>>> Detail(Guid id, CancellationToken token)
        {
            var result = await Mediator.Send(new WebHookDetailQuery { Id = id}, token);
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
        [HttpPost("[action]")]
        public async Task<ActionResult<Response<string>>> Create([FromBody] WebHookCreateCommand command, CancellationToken token)
        {
            var result = await Mediator.Send(command, token);
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
        [HttpPost("[action]")]
        public async Task<ActionResult<Response<Unit>>> Update([FromBody] WebHookUpdateCommand command, CancellationToken token)
        {
            var result = await Mediator.Send(command, token);
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
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Response<Unit>>> Delete(Guid id, CancellationToken token)
        {
            var result = await Mediator.Send(new WebHookDeleteCommand { Id = id}, token);
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
