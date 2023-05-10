using Application.Common.Interfaces;
using Application.Models;
using Application.Models.Account;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Functions.Accounts.Commands.ChangePassword
{
    public class ChangePasswordWithTFACommand : IRequest<Response<ChangePasswordResponseDto>>
    {
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? Code { get; set; }

        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class ChangePasswordWithTFACommandHandler : BaseHandler<ChangePasswordWithTFACommand, Response<ChangePasswordResponseDto>>
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;
        public ChangePasswordWithTFACommandHandler(ICommonService commonService,
            ILogger<ChangePasswordWithTFACommand> logger,
            IIdentityService identityService,
            ICurrentUserService currentUserService
            ) : base(commonService, logger)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }
        public async override Task<Response<ChangePasswordResponseDto>> Handle(ChangePasswordWithTFACommand request, CancellationToken cancellationToken)
        {
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var result = await _identityService.ChangePasswordWithTFAAsync(_currentUserService.UserId, request.CurrentPassword, request.NewPassword, request.Code);
#pragma warning restore CS8604 // Possible null reference argument.
                return Response<ChangePasswordResponseDto>.Success(result, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Update. Request: {Name} {@Request}", typeof(ChangePasswordWithTFACommand).Name, request);
                return new Response<ChangePasswordResponseDto>(false, ex.Message, ex.Message, "Failed to Update", request.requestId);
            }
        }
    }
}
