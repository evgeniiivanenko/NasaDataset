using Microsoft.EntityFrameworkCore;

namespace NasaDataset.Application.Common.Models
{
    public class PaginatedList<T>
    {
        public IReadOnlyCollection<T> Items { get;}

        public int PageNumber { get; }

        public int TotalPages { get; }

        public int TotalCount { get; }

        public PaginatedList(IReadOnlyCollection<T> item, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = item;
        }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var count = await source.Select(x => 1).CountAsync(cancellationToken);
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }

        public static async Task<PaginatedList<TResult>> CreateAsync<TIn, TResult>(IQueryable<TIn> source, int pageNumber, int pageSize, Func<TIn, TResult> selector, CancellationToken cancellationToken = default)
        {
            var count = await source.CountAsync(cancellationToken);
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PaginatedList<TResult>(items.Select(selector).ToList(), count, pageNumber, pageSize);
        }

        public static PaginatedList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize) {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
