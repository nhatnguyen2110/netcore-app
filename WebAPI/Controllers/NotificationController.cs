using Application.Functions.Notifications.Commands.WebPush;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Common;

namespace WebAPI.Controllers
{
    public class NotificationController : ApiControllerBase
    {
        [Authorize]
        [HttpPost("[action]")]
        public async Task<ActionResult<Response<Unit>>> WebPush([FromBody] WebPushCommand command, CancellationToken token)
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
    }
}
