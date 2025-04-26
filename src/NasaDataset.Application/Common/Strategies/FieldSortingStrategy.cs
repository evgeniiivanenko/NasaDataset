using NasaDataset.Application.Common.Interfaces;
using System.Linq.Expressions;

namespace NasaDataset.Application.Common.Strategies
{
    public class FieldSortingStrategy<T, TKey> : ISortingStrategy<T>
    {
        private readonly Expression<Func<T, TKey>> _selector;

        public FieldSortingStrategy(Expression<Func<T, TKey>> selector)
        {
            _selector = selector;
            Direction = "asc";
        }

        public string Direction { get; set; }

        public IQueryable<T> ApplySort(IQueryable<T> query)
        {
            return Direction == "asc"
                ? query.OrderBy(_selector)
                : query.OrderByDescending(_selector);
        }
    }
}
