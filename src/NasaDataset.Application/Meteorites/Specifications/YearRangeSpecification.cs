using NasaDataset.Application.Common.Interfaces;
using NasaDataset.Domain.Entities;

namespace NasaDataset.Application.Meteorites.Specifications
{
    public class YearRangeSpecification : ISpecification<Meteorite>
    {

        private readonly int? _yearFrom;
        private readonly int? _yearTo;

        public YearRangeSpecification(int? yearFrom, int? yearTo)
        {
            _yearFrom = yearFrom;
            _yearTo = yearTo;
        }

        public IQueryable<Meteorite> Apply(IQueryable<Meteorite> query)
        {
            if (_yearFrom != null)
                query = query.Where(m => m.Year >= _yearFrom);
            if (_yearTo != null)
                query = query.Where(m => m.Year <= _yearTo);
            return query;
        }

        public bool IsSatisfiedBy(Meteorite entity)
        {
            return (_yearFrom == null || entity.Year >= _yearFrom) &&
               (_yearTo == null || entity.Year <= _yearTo);
        }
    }
}
