using NasaDataset.Application.Common.Interfaces;
using NasaDataset.Domain.Entities;

namespace NasaDataset.Application.Meteorites.Specifications
{
    public class RecclassSpecification : ISpecification<Meteorite>
    {

        private readonly string _recclass;

        public RecclassSpecification(string recclas)
        {
            _recclass = recclas;
        }

        public IQueryable<Meteorite> Apply(IQueryable<Meteorite> query)
        {
            if(!string.IsNullOrEmpty(_recclass))
                query = query.Where(q => q.Recclass == _recclass);
            return query;
        }

        public bool IsSatisfiedBy(Meteorite entity)
        {
            return string.IsNullOrEmpty(_recclass) || entity.Recclass == _recclass;
        }
    }
}
