using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NasaDataset.Domain.Entities;

namespace NasaDataset.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Meteorite> Meteorites { get; }

        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken  cancellationToken);
    }
}
