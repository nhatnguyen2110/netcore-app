using Application.Common.Interfaces;
using Application.Models;
using Application.Models.Account;
using Application.Models.Settings;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Functions.Accounts.Commands.SignIn
{
    public class SignInCommand : UserForAuthenticationDto, IRequest<Response<SignInResultDto>>
    {
        public string? RecaptchaToken { get; set; }
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class SignInCommandHandler : BaseHandler<SignInCommand, Response<SignInResultDto>>
    {
        private readonly IIdentityService _identityService;
        private readonly ApplicationSettings _applicationSettings;
        public SignInCommandHandler(ICommonService commonService,
            ILogger<SignInCommand> logger,
            IIdentityService identityService,
            ApplicationSettings applicationSettings) : base(commonService, logger)
        {
            _identityService = identityService;
            _applicationSettings = applicationSettings;
        }
        public async override Task<Response<SignInResultDto>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (_applicationSettings.EnableEncryptAuthorize)
                {
                    try
                    {
#pragma warning disable CS8604 // Possible null reference argument.
                        request.UserName = CommonHelper.RSADecrypt(request.UserName, _applicationSettings.PrivateKeyEncode);
                        request.Password = CommonHelper.RSADecrypt(request.Password, _applicationSettings.PrivateKeyEncode);
#pragma warning restore CS8604 // Possible null reference argument.
                    }
                    catch (Exception ex)
                    {
                        return new Response<SignInResultDto>(false, "can not decrypt username and password", ex.Message, "Failed to Login", request.requestId);
                    }
                }
                var result = await this._identityService.AuthorizeAsync(request);

                return Response<SignInResultDto>.Success(result, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Login. Request: {Name} {@Request}", typeof(SignInCommand).Name, request);
                return new Response<SignInResultDto>(false, ex.Message, ex.Message, "Failed to Login", request.requestId);
            }
        }
    }
}
