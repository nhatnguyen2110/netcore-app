using Domain.Entities.Webhook;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Infrastructure.Persistence.Configuration
{
    public class WebHookConfiguration : IEntityTypeConfiguration<WebHook>
    {
        public void Configure(EntityTypeBuilder<WebHook> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Headers)
            .WithOne(e => e.WebHook)
            .HasForeignKey(e => e.WebHookID);

            builder.HasMany(e => e.Records)
            .WithOne(e => e.WebHook)
            .HasForeignKey(e => e.WebHookID);

#pragma warning disable CS8603 // Possible null reference return.
            builder.Property(x => x.HookEventTypes)
                .HasConversion(
                    new ValueConverter<List<string>, string>(
                        v => JsonConvert.SerializeObject(v), // Convert to string for persistence
                        v => JsonConvert.DeserializeObject<List<string>>(v)), // Convert to List<String> for use
                    new ValueComparer<List<string>>(
                        (c1, c2) => new HashSet<string>(c1!).SetEquals(new HashSet<string>(c2!)),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    )
                )
                ; 
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
