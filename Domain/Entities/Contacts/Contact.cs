using Domain.Common;
using Domain.Entities.Customers;

namespace Domain.Entities.Contacts
{
    public class Contact : DomainEntity<int>
    {
        public string? Name { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;

        virtual public IList<Customer> PrimaryContactCustomer { get; set; } = new List<Customer>();
        virtual public IList<Customer> OtherContactCustomer { get; set; } = new List<Customer>();
    }
}
