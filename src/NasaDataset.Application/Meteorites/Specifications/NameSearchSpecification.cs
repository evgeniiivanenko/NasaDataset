using NasaDataset.Application.Common.Interfaces;
using NasaDataset.Domain.Entities;

namespace NasaDataset.Application.Meteorites.Specifications
{
    public class NameSearchSpecification : ISpecification<Meteorite>
    {
        private readonly string _searchTerm;

        public NameSearchSpecification(string searchTerm)
        {
            _searchTerm = searchTerm?.ToLower();
        }

        public bool IsSatisfiedBy(Meteorite meteorite)
        {
            if (string.IsNullOrEmpty(_searchTerm))
                return true;

            return meteorite.Name?.ToLower().Contains(_searchTerm) ?? false;
        }

        public IQueryable<Meteorite> Apply(IQueryable<Meteorite> query)
        {
            if (string.IsNullOrEmpty(_searchTerm))
                return query;

            return query.Where(m => m.Name != null && m.Name.ToLower().Contains(_searchTerm));
        }
    }
}
