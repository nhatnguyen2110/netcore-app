using Application.Common.Interfaces;
using Application.Models;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Functions.Devices.Commands.Delete
{
    public class DeviceDeleteCommand : IRequest<Response<Unit>>
    {
        public int Id { get; set; }
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class DeviceDeleteCommandHandler : BaseHandler<DeviceDeleteCommand, Response<Unit>>
    {
        public DeviceDeleteCommandHandler(ICommonService commonService,
            ILogger<DeviceDeleteCommand> logger) : base(commonService, logger)
        {
        }
        public async override Task<Response<Unit>> Handle(DeviceDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _commonService.ApplicationDBContext.Devices.FirstOrDefaultAsync(x=>x.Id == request.Id);
                if(entity != null)
                {
                    _commonService.ApplicationDBContext.Devices.Remove(entity);
                    await _commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
                }
                return Response<Unit>.Success(Unit.Value, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete. Request: {Name} {@Request}", typeof(DeviceDeleteCommand).Name, request);
                return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to delete", request.requestId);
            }
        }
    }
}
