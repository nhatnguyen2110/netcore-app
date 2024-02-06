using Domain.Entities.Contacts;
using Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasMany<Customer>(p => p.PrimaryContactCustomer)
                .WithOne(c => c.PrimaryContact)
                .HasForeignKey(c => c.PrimaryContactId) // Nullable foreign key
                .IsRequired(false); // This makes the foreign key nullable

            builder.HasMany<Customer>(p => p.OtherContactCustomer)
                .WithOne(c => c.OtherContact)
                .HasForeignKey(c => c.OtherContactId) // Nullable foreign key
                .IsRequired(false); // This makes the foreign key nullable
        }
    }
}
