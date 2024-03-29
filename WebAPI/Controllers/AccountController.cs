﻿using Application.Common.Interfaces;
using Application.Functions.Accounts.Commands.ChangePassword;
using Application.Functions.Accounts.Commands.ExternalLogin;
using Application.Functions.Accounts.Commands.RefreshToken;
using Application.Functions.Accounts.Commands.ResetPassword;
using Application.Functions.Accounts.Commands.SignIn;
using Application.Functions.Accounts.Commands.SignUp;
using Application.Functions.Accounts.Commands.TFASetup;
using Application.Functions.Accounts.Queries.ResetPassword;
using Application.Functions.Accounts.Queries.TFASetup;
using Application.Models;
using Application.Models.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Common;

namespace WebAPI.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        public AccountController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
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
        public async Task<ActionResult<Response<SignInResultDto>>> SignIn([FromBody] SignInCommand command)
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
        [HttpPost("[action]")]
        public async Task<ActionResult<Response<SignInResultDto>>> SignInWithTFA([FromBody] SignInWithTFACommand command)
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
        [Authorize]
        [HttpGet("tfa-setup")]
        public async Task<ActionResult<Response<TFASetupDto>>> GetTFASetup()
        {
            var result = await Mediator.Send(new TFASetupQuery() { UserId = _currentUserService.UserId });
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
        [HttpPost("tfa-setup")]
        public async Task<ActionResult<Response<Unit>>> PostTFASetup(TFAEnableCommand command)
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
        [Authorize]
        [HttpDelete("tfa-setup")]
        public async Task<ActionResult<Response<Unit>>> DeleteTFASetup(TFADisableCommand command)
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
        [Authorize]
        [HttpPost("[action]")]
        public async Task<ActionResult<Response<ChangePasswordResponseDto>>> ChangePassword(ChangePasswordCommand command)
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
        [Authorize]
        [HttpPost("[action]")]
        public async Task<ActionResult<Response<ChangePasswordResponseDto>>> ChangePasswordWithTFA(ChangePasswordWithTFACommand command)
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
        [HttpPost("[action]")]
        public async Task<ActionResult<Response<Unit>>> SendTokenToResetPassword(ResetPasswordQuery query)
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
        [HttpPost("[action]")]
        public async Task<ActionResult<Response<Unit>>> ResetPassword(ResetPasswordCommand command)
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
		[HttpPost("[action]")]
		public async Task<ActionResult<Response<AuthTokenDto>>> RefreshToken(RefreshTokenCommand command)
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
        /// <summary>
        /// Single Sign On with Providers: Google, Facebook, Microsoft...
        /// return a custom token is used for API Service
        /// </summary>
        /// <param name="command">
        /// provider: Google, Facebook, Microsoft
        /// idToken: provider's token
        /// </param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Response<SignInResultDto>>> ExternalLogin(ExternalLoginCommand command)
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
