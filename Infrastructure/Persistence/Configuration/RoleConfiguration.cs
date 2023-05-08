using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
					Id = "f0967a26-d5ba-41a8-92e5-747783c4cc07",
					Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                },
                new IdentityRole
                {
					Id = "052997d8-20f2-4089-b5e1-50b296461d51",
					Name = "Member",
                    NormalizedName = "MEMBER"
                },
                new IdentityRole
                {
					Id = "d681023c-1441-4134-8253-f660cf26a716",
					Name = "Guest",
                    NormalizedName = "GUEST"
                }
                );
        }
    }
}
