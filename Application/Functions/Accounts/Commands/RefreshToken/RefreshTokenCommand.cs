using Application.Common.Interfaces;
using Application.Models;
using Application.Models.Account;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Functions.Accounts.Commands.RefreshToken
{
	public class RefreshTokenCommand : IRequest<Response<AuthTokenDto>>
	{
		public string? AccessToken { get; set; }
		public string? RefreshToken { get; set; }
		public string requestId { get; set; } = Guid.NewGuid().ToString();
	}
	public class RefreshTokenCommandHandler : BaseHandler<RefreshTokenCommand, Response<AuthTokenDto>>
	{
		private readonly IIdentityService _identityService;
		public RefreshTokenCommandHandler(ICommonService commonService,
			ILogger<RefreshTokenCommand> logger,
			IIdentityService identityService
			) : base(commonService, logger)
		{
			_identityService = identityService;
		}
		public async override Task<Response<AuthTokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			try
			{
#pragma warning disable CS8604 // Possible null reference argument.
				var result = await _identityService.RefreshTokenAsync(request.AccessToken, request.RefreshToken);
#pragma warning restore CS8604 // Possible null reference argument.
				return Response<AuthTokenDto>.Success(result, request.requestId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to Load. Request: {Name} {@Request}", typeof(RefreshTokenCommand).Name, request);
				return new Response<AuthTokenDto>(false, ex.Message, ex.Message, "Failed to Load", request.requestId);
			}
		}
	}
}
