using Application.Common.Interfaces;
using Application.Models;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebPush;

namespace Application.Functions.Notifications.Commands.WebPush
{
    public class WebPushCommand : IRequest<Response<Unit>>
    {
        public string? Payload { get; set; }
        public string? UserId { get; set; }
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class WebPushCommandHandler : BaseHandler<WebPushCommand, Response<Unit>>
    {
        private readonly IConfiguration _configuration;
        public WebPushCommandHandler(ICommonService commonService,
            IConfiguration configuration,
            ILogger<WebPushCommand> logger) : base(commonService, logger)
        {
            _configuration = configuration;
        }
        public async override Task<Response<Unit>> Handle(WebPushCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string vapidPublicKey = _configuration.GetSection("VapidKeys")["PublicKey"]??"";
                string vapidPrivateKey = _configuration.GetSection("VapidKeys")["PrivateKey"] ?? "";
                string vapidSubject = _configuration.GetSection("VapidKeys")["Subject"] ?? "";
                var vapidDetails = new VapidDetails("mailto:example@example.com", vapidPublicKey, vapidPrivateKey);
                var devices = await _commonService.ApplicationDBContext.Devices.AsNoTracking().Where(x => x.UserId == request.UserId).ToListAsync();
                foreach (var device in devices)
                {
                    var pushSubscription = new PushSubscription(device.PushEndpoint, device.PushP256DH, device.PushAuth);
                    var webPushClient = new WebPushClient();
                    webPushClient.SendNotification(pushSubscription, request.Payload, vapidDetails);
                }
                return Response<Unit>.Success(Unit.Value, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send. Request: {Name} {@Request}", typeof(WebPushCommand).Name, request);
                return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to send", request.requestId);
            }
        }
    }
}
