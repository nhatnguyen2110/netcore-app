﻿using Application.Common.Interfaces;
using Application.Models;
using Application.Models.Account;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Functions.Accounts.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Response<ChangePasswordResponseDto>>
    {
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }

        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class ChangePasswordCommandHandler : BaseHandler<ChangePasswordCommand, Response<ChangePasswordResponseDto>>
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;
        public ChangePasswordCommandHandler(ICommonService commonService,
            ILogger<ChangePasswordCommand> logger,
            IIdentityService identityService,
            ICurrentUserService currentUserService
            ) : base(commonService, logger)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }
        public async override Task<Response<ChangePasswordResponseDto>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var result = await _identityService.ChangePasswordAsync(_currentUserService.UserId, request.CurrentPassword, request.NewPassword);
#pragma warning restore CS8604 // Possible null reference argument.
                return Response<ChangePasswordResponseDto>.Success(result, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Update. Request: {Name} {@Request}", typeof(ChangePasswordCommand).Name, request);
                return new Response<ChangePasswordResponseDto>(false, ex.Message, ex.Message, "Failed to Update", request.requestId);
            }
        }
    }
}
