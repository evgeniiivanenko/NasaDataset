using NasaDataset.Application.Common.Models;
using NasaDataset.Domain.Entities;
using NasaDataset.Domain.ValueObjects;

namespace NasaDataset.Application.Common.Interfaces
{
    public interface IMeteoriteRepository
    {
        Task<PaginatedList<TResult>> GetFilteredAsync<TResult>(
            ISpecification<Meteorite> specification,
            ISortingStrategy<Meteorite> sortingStrategy,
            Func<Meteorite, TResult> selecter,
            int pageNumber,
            int pageSize,
            CancellationToken ct = default);

        Task<PaginatedList<TResult>> GetGroupedAsync<TResult>(
            ISpecification<Meteorite> specification,
            ISortingStrategy<TResult> sortingStrategy,
            IGroupingStrategy<Meteorite, TResult> groupingStrategy,
            int pageNumber,
            int pageSize,
            CancellationToken ct = default) where TResult : class;

        Task AddRangeAsync(IEnumerable<Meteorite> meteorites, CancellationToken ct = default);
        Task DeleteRangeAsync(List<ExternalMeteoriteId> idsToRemove, CancellationToken ct = default);
        Task<List<string>> GetDistinctRecclassesAsync(CancellationToken cancellationToken);
        Task<List<int>> GetDistinctYearsAsync(CancellationToken cancellationToken);
        Task<List<ExternalMeteoriteId>> GetExternalIdsAsync(CancellationToken cancellationToken);
    }
}
