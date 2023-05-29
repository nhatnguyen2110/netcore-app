using Application.Functions.Devices.Queries.DeviceList;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Functions.Notifications.TestNotifies
{
    public class Ping : BaseEvent { }
    public class Pong1 : INotificationHandler<Ping>
    {
        private readonly ILogger _logger;
        public Pong1(ILogger<GetDevicesQuery> logger) { 
            _logger= logger;
        }
        public Task Handle(Ping notification, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Ping 1");
            Thread.Sleep(5000);
            return Task.CompletedTask;
        }
    }

    public class Pong2 : INotificationHandler<Ping>
    {
        private readonly ILogger _logger;
        public Pong2(ILogger<GetDevicesQuery> logger)
        {
            _logger = logger;
        }
        public Task Handle(Ping notification, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Ping 2");
            Thread.Sleep(5000);
            return Task.CompletedTask;
        }
    }
}
