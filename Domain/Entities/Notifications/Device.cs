using Domain.Common;

namespace Domain.Entities.Notifications
{
    public class Device : DomainEntity<int>
    {
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? PushEndpoint { get; set; }
        public string? PushP256DH { get; set; }
        public string? PushAuth { get; set; }
    }
}
