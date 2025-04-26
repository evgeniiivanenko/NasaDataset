using Microsoft.EntityFrameworkCore;
using NasaDataset.Application.Common.Models;

namespace NasaDataset.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> queryable, int pageNumber, int pageSize, CancellationToken cancellationToken = default) where T : class
         => PaginatedList<T>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize, cancellationToken);

        public static Task<PaginatedList<TResult>> ToPaginatedListAsync<TIn, TResult>(this IQueryable<TIn> queryable, int pageNumber, int pageSize, Func<TIn, TResult> selector, CancellationToken cancellationToken = default) where TIn : class
         => PaginatedList<TResult>.CreateAsync(queryable, pageNumber, pageSize, selector, cancellationToken);

        public static PaginatedList<T> ToPaginatedList<T>(this IEnumerable<T> queryable, int pageNumber, int pageSize) where T : class
         => PaginatedList<T>.Create(queryable, pageNumber, pageSize);
    }
}
