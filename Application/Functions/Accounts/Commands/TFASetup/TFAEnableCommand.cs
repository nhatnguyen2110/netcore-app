using Application.Common.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Functions.Accounts.Commands.TFASetup
{
    public class TFAEnableCommand : IRequest<Response<Unit>>
    {
        public string Email { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class TFAEnableCommandHandler : BaseHandler<TFAEnableCommand, Response<Unit>>

    {
        private readonly IIdentityService _identityService;
        public TFAEnableCommandHandler(ICommonService commonService,
            ILogger<TFAEnableCommand> logger,
            IIdentityService identityService) : base(commonService, logger)
        {
            _identityService = identityService;
        }
        public async override Task<Response<Unit>> Handle(TFAEnableCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _identityService.EnableTFAAsync(request.Email, request.Code);
                return Response<Unit>.Success(Unit.Value, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to setup. Request: {Name} {@Request}", typeof(TFAEnableCommand).Name, request);
                return new Response<Unit>(false, ex.Message, ex.Message, "Failed to setup", request.requestId);
            }
        }
    }
}
