﻿using Application.Common.Interfaces;
using Application.Models;
using Application.Models.Account;
using Domain;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Functions.Accounts.Commands.SignUp
{
    public class SignUpCommand : UserForRegistrationDto, IRequest<Response<Unit>>
    {
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class SignUpCommandHandler : BaseHandler<SignUpCommand, Response<Unit>>
    {
        private readonly IIdentityService _identityService;
        public SignUpCommandHandler(ICommonService commonService,
            ILogger<SignUpCommand> logger,
            IIdentityService identityService
            ) : base(commonService, logger)
        {
            _identityService = identityService;
        }
        public async override Task<Response<Unit>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _identityService.CreateUserAsync(request, new[] { RoleList.Member.ToString() });
                return Response<Unit>.Success(Unit.Value, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Sign Up. Request: {Name} {@Request}", typeof(SignUpCommand).Name, request);
                return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Sign Up", request.requestId);
            }
        }
    }
}
