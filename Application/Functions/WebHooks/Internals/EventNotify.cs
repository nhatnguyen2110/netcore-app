using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Functions.WebHooks.Internals
{
    public class EventNotify : BaseEvent
    {
        public HookEventType HookEventType { get; set; }
        public object? PayLoad { get; set; }
        public ICommonService CommonService { get; set; }
    }
    public class EventNotifyHandler : INotificationHandler<EventNotify>
    {
        protected readonly ILogger _logger;
        private readonly IMediator _mediator;
        public EventNotifyHandler(IApplicationDbContext applicationDbContext,
            IMediator mediator,
            ILogger<EventNotify> logger) { 
            _logger= logger;
            _mediator= mediator;
        }
        public async Task Handle(EventNotify notification, CancellationToken cancellationToken)
        {
            try
            {
                var hooks = await notification.CommonService.ApplicationDBContext.WebHooks
                    .AsNoTracking()
                    .Where(x => !x.Deleted && x.IsActive)
                    .ToListAsync(cancellationToken);
                // consider to use cronjob to process
                var tasks = hooks.Where(x => x.HookEventTypes.Contains(notification.HookEventType.ToString()))
                    .Select(h => Process(notification, h.Id, cancellationToken));
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {@Request}", typeof(EventNotify).Name, notification);
            }
        }
        private async Task Process(EventNotify notification, Guid webHookId, CancellationToken cancellationToken) {
            await _mediator.Send(new ProcessWebHook() { WebHookId = webHookId, HookEventType = notification.HookEventType, PayLoad = notification.PayLoad }, cancellationToken);
        }
    }
}
