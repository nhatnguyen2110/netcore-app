using Application.Common.DBSupports;
using Domain.Entities.Forms;
using Domain.Entities.Log;
using Domain.Entities.Notifications;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<FormData> FormDatas { get; }
        DbSet<AuditLog> AuditLogs { get; }
        DbSet<Device> Devices { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
		DbSet<SPSingleValueQueryResultString> GetSingleValueQueryString { get; set; }
        DbSet<SPColumnTypes> GetSPColumnTypes { get; set; }
    }
}
