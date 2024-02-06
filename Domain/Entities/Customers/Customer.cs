using Domain.Common;
using Domain.Entities.Contacts;

namespace Domain.Entities.Customers
{
    public class Customer : DomainEntity<int>
    {
        public string? Name { get; set; }
        public int? PrimaryContactId { get; set; }
        public int? OtherContactId { get; set; }

        public Contact? PrimaryContact { get; set; }
        public Contact? OtherContact { get; set; }

    }
}
