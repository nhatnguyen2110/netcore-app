using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.Notifications;

namespace Application.Models.Devices
{
    public class DeviceDto : IMapFrom<Device>
    {
        public string? Name { get; set; }
        public string? PushEndpoint { get; set; }
        public string? PushP256DH { get; set; }
        public string? PushAuth { get; set; }
    }
}
