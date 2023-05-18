using Application.Common.Interfaces;
using Application.Models;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Functions.Devices.Commands.Delete
{
    public class DeviceDeleteByUserIdCommand : IRequest<Response<Unit>>
    {
        public string UserId { get; set; } = string.Empty;
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class DeviceDeleteByUserIdCommandHandler : BaseHandler<DeviceDeleteByUserIdCommand, Response<Unit>>
    {
        public DeviceDeleteByUserIdCommandHandler(ICommonService commonService,
            ILogger<DeviceDeleteByUserIdCommand> logger) : base(commonService, logger)
        {
        }
        public async override Task<Response<Unit>> Handle(DeviceDeleteByUserIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entities = await _commonService.ApplicationDBContext.Devices.Where(x => x.UserId == request.UserId).ToListAsync();
                foreach(var entity in entities) 
                {
                    _commonService.ApplicationDBContext.Devices.Remove(entity);
                    await _commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
                }
                return Response<Unit>.Success(Unit.Value, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete. Request: {Name} {@Request}", typeof(DeviceDeleteByUserIdCommand).Name, request);
                return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to delete", request.requestId);
            }
        }
    }
}
