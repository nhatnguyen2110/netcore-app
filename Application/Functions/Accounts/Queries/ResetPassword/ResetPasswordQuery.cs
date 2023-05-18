using Application.Common.Interfaces;
using Application.Models;
using Application.Models.Settings;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Web;

namespace Application.Functions.Accounts.Queries.ResetPassword
{
    public class ResetPasswordQuery : IRequest<Response<Unit>>
    {
        public string? Email { get; set; }
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class ResetPasswordQueryHandler : BaseHandler<ResetPasswordQuery, Response<Unit>>
    {
        private readonly IIdentityService _identityService;
        private readonly ApplicationSettings _applicationSettings;
        public ResetPasswordQueryHandler(ICommonService commonService,
            ILogger<ResetPasswordQuery> logger,
            ApplicationSettings applicationSettings,
            IIdentityService identityService) : base(commonService, logger)
        {
            _identityService = identityService;
            _applicationSettings = applicationSettings;
        }
        public async override Task<Response<Unit>> Handle(ResetPasswordQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tokenData = await _identityService.GetTokenPasswordResetAsync(request.Email ?? "");
                // send email
#pragma warning disable CS8604 // Possible null reference argument.
                _commonService.EmailService.SendEmail(
                    _applicationSettings.EmailResetPassword_Subject,
                    String.Format(_applicationSettings.EmailResetPassword_Content, $"{HttpUtility.UrlEncode(tokenData.UserId)}/{HttpUtility.UrlEncode(tokenData.Token)}"),
                    request.Email
                    );
#pragma warning restore CS8604 // Possible null reference argument.
                return Response<Unit>.Success(Unit.Value, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load. Request: {Name} {@Request}", typeof(ResetPasswordQuery).Name, request);
                return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to load", request.requestId);
            }
        }
    }
}
