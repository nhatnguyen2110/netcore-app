using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Models;
using Application.Models.Devices;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;

namespace Application.Functions.Devices.Queries.DeviceList
{
    public class GetDevicesQuery : RequestParameter, IRequest<Response<PaginatedList<DeviceDto>>>
    {
        public string requestId { get; set; } = Guid.NewGuid().ToString();
    }
    public class GetDevicesQueryHandler : BaseHandler<GetDevicesQuery, Response<PaginatedList<DeviceDto>>>
    {
        private readonly ICurrentUserService _currentUserService;
        public GetDevicesQueryHandler(ICommonService commonService,
            ICurrentUserService currentUserService,
            ILogger<GetDevicesQuery> logger) : base(commonService, logger)
        {
            _currentUserService = currentUserService;
        }
        public async override Task<Response<PaginatedList<DeviceDto>>> Handle(GetDevicesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _commonService.ApplicationDBContext.Devices
                    .AsNoTracking()
                    .Where(x => x.UserId == _currentUserService.UserId)
                    .OrderBy(request.OrderBy)
                    .ProjectTo<DeviceDto>(this._commonService.Mapper?.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
                return Response<PaginatedList<DeviceDto>>.Success(result, request.requestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load. Request: {Name} {@Request}", typeof(GetDevicesQuery).Name, request);
                return new Response<PaginatedList<DeviceDto>>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to load", request.requestId);
            }
        }
    }
}
