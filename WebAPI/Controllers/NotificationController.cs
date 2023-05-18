using Application.Common.Interfaces;
using Application.Functions.Accounts.Commands.SignIn;
using Application.Functions.Notifications.Commands.WebPush;
using Application.Models;
using Application.Models.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Common;

namespace WebAPI.Controllers
{
    public class NotificationController : ApiControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        public NotificationController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        [Authorize]
        [HttpPost("[action]")]
        public async Task<ActionResult<Response<Unit>>> WebPush([FromBody] WebPushCommand command)
        {
            var result = await Mediator.Send(command);
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
