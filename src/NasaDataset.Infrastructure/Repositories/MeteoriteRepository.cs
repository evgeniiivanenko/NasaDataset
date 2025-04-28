using Microsoft.EntityFrameworkCore;
using NasaDataset.Application.Common.Interfaces;
using NasaDataset.Application.Common.Mappings;
using NasaDataset.Application.Common.Models;
using NasaDataset.Domain.Entities;
using NasaDataset.Domain.ValueObjects;

namespace NasaDataset.Infrastructure.Repositories
{
    public class MeteoriteRepository : IMeteoriteRepository
    {
        private readonly IApplicationDbContext _context;

        public MeteoriteRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<TResult>> GetFilteredAsync<TResult>(
            ISpecification<Meteorite> specification,
            ISortingStrategy<Meteorite> sorting,
            Func<Meteorite, TResult> selector,
            int pageNumber,
            int pageSize,
            CancellationToken ct = default) 
        {
            var query = _context.Meteorites.AsQueryable();
            query = specification.Apply(query);

            query = sorting.ApplySort(query);


            return await query.ToPaginatedListAsync(pageNumber, pageSize, selector, ct);
        }

        public async Task AddRangeAsync(IEnumerable<Meteorite> meteorites, CancellationToken ct = default)
        {
            await _context.Meteorites.AddRangeAsync(meteorites, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteRangeAsync(List<ExternalMeteoriteId> idsToRemove, CancellationToken ct = default)
        {
            if (idsToRemove == null) throw new ArgumentNullException(nameof(idsToRemove));

            if (idsToRemove.Any())
            {
                await _context.Meteorites
                .Where(m => idsToRemove.Contains(m.ExternalId))
                .ExecuteDeleteAsync(ct)
                .ConfigureAwait(false);

                await _context.SaveChangesAsync(ct).ConfigureAwait(false);
            }
        }

        public async Task<List<string>> GetDistinctRecclassesAsync(CancellationToken cancellationToken)
        {
            return await _context.Meteorites
                .Where(x => x.Recclass != null)
                .Select(x => x.Recclass!)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<int>> GetDistinctYearsAsync(CancellationToken cancellationToken)
        {
            return await _context.Meteorites
                .Where(x => x.Year != null)
                .Select(x => x.Year!.Value)
                .Distinct()
                .OrderByDescending(x => x)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<ExternalMeteoriteId>> GetExternalIdsAsync(CancellationToken cancellationToken)
            => await _context.Meteorites.Select(x => x.ExternalId).ToListAsync(cancellationToken);

        public async Task<PaginatedList<TResult>> GetGroupedAsync<TResult>(ISpecification<Meteorite> specification, ISortingStrategy<TResult> sortingStrategy, IGroupingStrategy<Meteorite, TResult> groupingStrategy, int pageNumber, int pageSize, CancellationToken ct = default) where TResult : class
        {
            var query = _context.Meteorites.AsQueryable();

            query = specification.Apply(query);

            var groupedQuery = groupingStrategy.Group(query);
            groupedQuery = sortingStrategy.ApplySort(groupedQuery);

            return await groupedQuery.ToPaginatedListAsync(pageNumber, pageSize, ct);
        }
    }
}
