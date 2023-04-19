using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Functions
{
    public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        protected readonly ICommonService _commonService;
        protected readonly ILogger _logger;
        public BaseHandler(ICommonService commonService,
            ILogger<TRequest> logger
            )
        {
            _commonService = commonService;
            _logger = logger;
        }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public BaseHandler(ICommonService commonService
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            )
        {
            _commonService = commonService;
        }
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
