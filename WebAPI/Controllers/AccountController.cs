using Application.Functions.Accounts.Commands.SignIn;
using Application.Functions.Accounts.Commands.SignUp;
using Application.Models;
using Application.Models.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Common;

namespace WebAPI.Controllers
{
    public class AccountController : ApiControllerBase
    {
        [HttpPost("[action]")]
        public async Task<ActionResult<Response<Unit>>> SignUp([FromBody] SignUpCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.Succeeded)
            {
                return StatusCode(201);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Response<SignInResultDto>>> AuthenticateUser([FromBody] SignInCommand command)
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
