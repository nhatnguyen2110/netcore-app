using Application.Common.Interfaces;
using Application.Models;
using Application.Models.Account;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Functions.Accounts.Commands.SignIn
{
    public class SignInWithTFACommand : UserForTFAAuthDto, IRequest<Response<SignInResultDto>>
    {
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class SignInWithTFACommandHandler : BaseHandler<SignInWithTFACommand, Response<SignInResultDto>>
    {
        private readonly IIdentityService _identityService;
        public SignInWithTFACommandHandler(ICommonService commonService,
            ILogger<SignInWithTFACommand> logger,
            IIdentityService identityService) : base(commonService, logger)
        {
            _identityService = identityService;
        }
        public async override Task<Response<SignInResultDto>> Handle(SignInWithTFACommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._identityService.AuthorizeTFAAsync(request);

                return Response<SignInResultDto>.Success(result, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Login. Request: {Name} {@Request}", typeof(SignInWithTFACommand).Name, request);
                return new Response<SignInResultDto>(false, ex.Message, ex.Message, "Failed to Login", request.requestId);
            }
        }
    }
}
