using Domain.Entities.Forms;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<FormData> FormDatas { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
