using Application.Common.DBSupports;
using Domain.Entities.Forms;
using Domain.Entities.Log;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<FormData> FormDatas { get; }
        DbSet<AuditLog> AuditLogs { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
		DbSet<SPSingleValueQueryResultString> GetSingleValueQueryString { get; set; }
        DbSet<SPColumnTypes> SPColumnTypes { get; set; }
    }
}
