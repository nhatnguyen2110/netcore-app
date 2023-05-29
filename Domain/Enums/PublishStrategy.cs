using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    /// <summary>
    /// Strategy to use when publishing notifications
    /// </summary>
    public enum PublishStrategy
    {
        /// <summary>
        /// Run each notification handler after one another. Returns when all handlers are finished or an exception has been thrown. In case of an exception, any handlers after that will not be run.
        /// </summary>
        SyncContinueOnException = 0,

        /// <summary>
        /// Run each notification handler after one another. Returns when all handlers are finished. In case of any exception(s), they will be captured in an AggregateException.
        /// </summary>
        SyncStopOnException = 1,

        /// <summary>
        /// Run all notification handlers asynchronously. Returns when all handlers are finished. In case of any exception(s), they will be captured in an AggregateException.
        /// </summary>
        Async = 2,

        /// <summary>
        /// Run each notification handler on its own thread using Task.Run(). Returns when all threads (handlers) are finished. In case of any exception(s), if the call to Publish is awaited, they are captured in an AggregateException by Task.WhenAll. Do not use this strategy if you're accessing the database in your handlers, DbContext is not thread-safe.
        /// </summary>
        ParallelNoWait = 3,

        /// <summary>
        /// Create a single new thread using Task.Run(), and run all notifications sequentially (continue on exception). Returns immediately and does not wait for any handlers to finish. Note that you cannot capture any exceptions, even if you await the call to Publish. To improve the traceability the exception is being captured internally and logged with ILogger if available.
        /// </summary>
        ParallelWhenAll = 4,

        /// <summary>
        /// Run each notification handler on its own thread using Task.Run(). Returns immediately and does not wait for any handlers to finish. Note that you cannot capture any exceptions, even if you await the call to Publish. To improve the traceability the exception is being captured internally and logged with ILogger if available. Do not use this strategy if you're accessing the database in your handlers, DbContext is not thread-safe.
        /// </summary>
        ParallelWhenAny = 5,
    }
}
