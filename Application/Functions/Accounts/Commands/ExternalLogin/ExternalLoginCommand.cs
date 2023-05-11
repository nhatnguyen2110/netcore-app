using Application.Common.Interfaces;
using Application.Functions.Accounts.Commands.ChangePassword;
using Application.Models;
using Application.Models.Account;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Functions.Accounts.Commands.ExternalLogin
{
    public class ExternalLoginCommand  : ExternalAuthDto, IRequest<Response<SignInResultDto>>
    {
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class ExternalLoginCommandHandler : BaseHandler<ExternalLoginCommand, Response<SignInResultDto>>
    {
        private readonly IIdentityService _identityService;
        public ExternalLoginCommandHandler(ICommonService commonService,
            ILogger<ExternalLoginCommand> logger,
            IIdentityService identityService
            ) : base(commonService, logger)
        {
            _identityService = identityService;
        }
        public async override Task<Response<SignInResultDto>> Handle(ExternalLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                switch (request.Provider)
                {
                    case "GOOGLE":
                        return Response<SignInResultDto>.Success(await _identityService.GoogleLoginAsync(request), request.requestId);
                    default:
                        throw new Exception("Invalid Provider");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Load. Request: {Name} {@Request}", typeof(ExternalLoginCommand).Name, request);
                return new Response<SignInResultDto>(false, ex.Message, ex.Message, "Failed to Load", request.requestId);
            }
        }
    }
}
