using Domain.Entities.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class FormDataConfiguration : IEntityTypeConfiguration<FormData>
    {
        public void Configure(EntityTypeBuilder<FormData> builder)
        {
        }
    }
}
