using Application.Common.Interfaces;
using Application.Functions.Devices.Commands.Create;
using Application.Functions.Devices.Commands.Delete;
using Application.Functions.Devices.Queries.DeviceList;
using Application.Models;
using Application.Models.Devices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Common;

namespace WebAPI.Controllers
{
    public class DeviceController : ApiControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        public DeviceController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        [Authorize]
        [HttpGet("[action]")]
        public async Task<ActionResult<Response<PaginatedList<DeviceDto>>>> List([FromQuery] GetDevicesQuery query, CancellationToken token)
        {
            var result = await Mediator.Send(query, token);
            if (result.Succeeded)
            {
                return result;
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Response<int>>> Create([FromBody] DeviceCreateCommand request, CancellationToken token)
        {
            var result = await Mediator.Send(request, token);
            if (result.Succeeded)
            {
                return result;
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize]
        [HttpDelete("[action]")]
        public async Task<ActionResult<Response<Unit>>> DeleteUserDevices(CancellationToken token)
        {
            var result = await Mediator.Send(new DeviceDeleteByUserIdCommand() { UserId = _currentUserService.UserId ?? "" }, token);
            if (result.Succeeded)
            {
                return result;
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpDelete("[action]")]
        public async Task<ActionResult<Response<Unit>>> Delete([FromQuery] DeviceDeleteCommand request, CancellationToken token)
        {
            var result = await Mediator.Send(request, token);
            if (result.Succeeded)
            {
                return result;
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
