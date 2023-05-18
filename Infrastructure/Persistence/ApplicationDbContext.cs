using Application.Common.DBSupports;
using Application.Common.Interfaces;
using Domain.Entities.Forms;
using Domain.Entities.Log;
using Domain.Entities.Notifications;
using Domain.Entities.User;
using Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
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
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
		public virtual DbSet<SPSingleValueQueryResultString> GetSingleValueQueryString { get; set; }
        public virtual DbSet<SPColumnTypes> GetSPColumnTypes { get; set; }
        public DbSet<Device> Devices => Set<Device>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			builder.Entity<SPSingleValueQueryResultString>().HasNoKey().ToTable(nameof(SPSingleValueQueryResultString), t => t.ExcludeFromMigrations());
            builder.Entity<SPColumnTypes>().HasNoKey().ToTable(nameof(SPColumnTypes), t => t.ExcludeFromMigrations());

            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }
    }
}
