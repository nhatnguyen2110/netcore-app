using Application.Functions;
using Domain.Enums;
using MediatR;

namespace Application.Common.MediatR
{
    internal class CustomPublisher : INotificationPublisher
    {
        public async Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
        {
            if (notification is BaseEvent)
            {
                BaseEvent e = (BaseEvent)notification;

                if (e.PublishStrategy == PublishStrategy.Async)
                {
                    await AsyncContinueOnException(handlerExecutors, notification, cancellationToken);
                }
                else if (e.PublishStrategy == PublishStrategy.SyncStopOnException)
                {
                    await SyncStopOnException(handlerExecutors, notification, cancellationToken);
                }
                else if (e.PublishStrategy == PublishStrategy.SyncContinueOnException)
                {
                    await SyncContinueOnException(handlerExecutors, notification, cancellationToken);
                }
                else if (e.PublishStrategy == PublishStrategy.ParallelNoWait)
                {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    ParallelNoWait(handlerExecutors, notification, cancellationToken);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                }
                else if (e.PublishStrategy == PublishStrategy.ParallelWhenAny)
                {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    ParallelWhenAny(handlerExecutors, notification, cancellationToken);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                }
                else if (e.PublishStrategy == PublishStrategy.ParallelWhenAll)
                {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    ParallelWhenAll(handlerExecutors, notification, cancellationToken);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                }
                else
                {
                    throw new ArgumentException("Unknown publish strategy.");
                }
            }
            else
            {
                throw new ArgumentException("Notification event class should inherit BaseEvent.");
            }
        }

        private Task ParallelWhenAll(IEnumerable<NotificationHandlerExecutor> handlers, INotification notification, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();

            foreach (var handler in handlers)
            {
                tasks.Add(Task.Run(() => handler.HandlerCallback(notification, cancellationToken)));
            }

            return Task.WhenAll(tasks);
        }

        private Task ParallelWhenAny(IEnumerable<NotificationHandlerExecutor> handlers, INotification notification, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();

            foreach (var handler in handlers)
            {
                tasks.Add(Task.Run(() => handler.HandlerCallback(notification, cancellationToken)));
            }

            return Task.WhenAny(tasks);
        }

        private Task ParallelNoWait(IEnumerable<NotificationHandlerExecutor> handlers, INotification notification, CancellationToken cancellationToken)
        {
            foreach (var handler in handlers)
            {
                Task.Run(() => handler.HandlerCallback(notification, cancellationToken));
            }

            return Task.CompletedTask;
        }

        private async Task AsyncContinueOnException(IEnumerable<NotificationHandlerExecutor> handlers, INotification notification, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();
            var exceptions = new List<Exception>();

            foreach (var handler in handlers)
            {
                try
                {
                    tasks.Add(handler.HandlerCallback(notification, cancellationToken));
                }
                catch (Exception ex) when (!(ex is OutOfMemoryException || ex is StackOverflowException))
                {
                    exceptions.Add(ex);
                }
            }

            try
            {
                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
            catch (AggregateException ex)
            {
                exceptions.AddRange(ex.Flatten().InnerExceptions);
            }
            catch (Exception ex) when (!(ex is OutOfMemoryException || ex is StackOverflowException))
            {
                exceptions.Add(ex);
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }

        private async Task SyncStopOnException(IEnumerable<NotificationHandlerExecutor> handlers, INotification notification, CancellationToken cancellationToken)
        {
            foreach (var handler in handlers)
            {
                await handler.HandlerCallback(notification, cancellationToken).ConfigureAwait(false);
            }
        }

        private async Task SyncContinueOnException(IEnumerable<NotificationHandlerExecutor> handlers, INotification notification, CancellationToken cancellationToken)
        {
            var exceptions = new List<Exception>();

            foreach (var handler in handlers)
            {
                try
                {
                    await handler.HandlerCallback(notification, cancellationToken).ConfigureAwait(false);
                }
                catch (AggregateException ex)
                {
                    exceptions.AddRange(ex.Flatten().InnerExceptions);
                }
                catch (Exception ex) when (!(ex is OutOfMemoryException || ex is StackOverflowException))
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
