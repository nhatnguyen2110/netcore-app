using Application.Models.Account;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Functions.Accounts.Commands.SignIn;
using Application.Models.Settings;
using Domain.Common;
using Microsoft.Extensions.Logging;
using Domain;

namespace Application.Functions.Accounts.Queries.TFASetup
{
    public class TFASetupQuery : IRequest<Response<TFASetupDto>>
    {
        public string? UserId { get; set; }
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class TFASetupQueryHandler : BaseHandler<TFASetupQuery, Response<TFASetupDto>>
    {
        private readonly IIdentityService _identityService;
        public TFASetupQueryHandler(ICommonService commonService,
            ILogger<TFASetupQuery> logger,
            IIdentityService identityService) : base(commonService, logger)
        {
            _identityService = identityService;
        }
        public async override Task<Response<TFASetupDto>> Handle(TFASetupQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _identityService.GetTFASetupAsync(request.UserId ?? "");
                return Response<TFASetupDto>.Success(result, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load. Request: {Name} {@Request}", typeof(SignInCommand).Name, request);
                return new Response<TFASetupDto>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to load", request.requestId);
            }
        }
    }
}
