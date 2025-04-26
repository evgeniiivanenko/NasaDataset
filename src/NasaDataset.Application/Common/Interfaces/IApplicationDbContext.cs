using Microsoft.EntityFrameworkCore;
using NasaDataset.Domain.Entities;

namespace NasaDataset.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Meteorite> Meteorites { get; }

        Task<int> SaveChangesAsync(CancellationToken  cancellationToken);
    }
}
