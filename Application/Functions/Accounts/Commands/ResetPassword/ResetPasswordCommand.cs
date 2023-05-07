using Application.Common.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Functions.Accounts.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<Response<Unit>>
    {
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public string? newPassword { get; set; }
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class ResetPasswordCommandHandler : BaseHandler<ResetPasswordCommand, Response<Unit>>
    {
        private readonly IIdentityService _identityService;
        public ResetPasswordCommandHandler(ICommonService commonService,
            ILogger<ResetPasswordCommand> logger,
            IIdentityService identityService
            ) : base(commonService, logger)
        {
            _identityService = identityService;
        }
        public async override Task<Response<Unit>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                await _identityService.ResetPasswordAsync(request.UserId, request.Token, request.newPassword);
#pragma warning restore CS8604 // Possible null reference argument.
                return Response<Unit>.Success(Unit.Value, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Update. Request: {Name} {@Request}", typeof(ResetPasswordCommand).Name, request);
                return new Response<Unit>(false, ex.Message, ex.Message, "Failed to Update", request.requestId);
            }
        }
    }
}
