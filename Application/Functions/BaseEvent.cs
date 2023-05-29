using Domain.Enums;
using MediatR;

namespace Application.Functions
{
    public abstract class BaseEvent : INotification
    {
        public PublishStrategy PublishStrategy { get; set; } = PublishStrategy.Async;
    }
}
