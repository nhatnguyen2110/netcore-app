using Application.Common.Interfaces;
using Domain.Entities.Forms;
using Domain.Entities.User;
using Infrastructure.Persistance.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
        public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor
            )
        : base(options)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }
        public DbSet<FormData> FormDatas => Set<FormData>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }
    }
}
