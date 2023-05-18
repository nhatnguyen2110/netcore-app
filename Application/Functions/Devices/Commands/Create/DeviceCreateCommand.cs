using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Models;
using Application.Models.Devices;
using Domain;
using Domain.Entities.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Functions.Devices.Commands.Create
{
    public class DeviceCreateCommand : IRequest<Response<int>>
    {
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? PushEndpoint { get; set; }
        public string? PushP256DH { get; set; }
        public string? PushAuth { get; set; }
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class DeviceCreateCommandHandler : BaseHandler<DeviceCreateCommand, Response<int>>
    {
        public DeviceCreateCommandHandler(ICommonService commonService,
            ILogger<DeviceCreateCommand> logger) : base(commonService, logger)
        {
        }
        public async override Task<Response<int>> Handle(DeviceCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Device
                {
                    UserId = request.UserId,
                    Name = request.Name,
                    PushEndpoint = request.PushEndpoint,
                    PushAuth = request.PushAuth,
                    PushP256DH = request.PushP256DH,
                };
                _commonService.ApplicationDBContext.Devices.Add(entity);
                await _commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
                return Response<int>.Success(entity.Id, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create. Request: {Name} {@Request}", typeof(DeviceCreateCommand).Name, request);
                return new Response<int>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to create", request.requestId);
            }
        }
    }
 }
