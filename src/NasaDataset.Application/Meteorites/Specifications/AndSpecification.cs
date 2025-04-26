using NasaDataset.Application.Common.Interfaces;

namespace NasaDataset.Application.Meteorites.Specifications
{
    public  class AndSpecification<T> : ISpecification<T>
    {
        private readonly IEnumerable<ISpecification<T>> _specifications;

        public AndSpecification(params ISpecification<T>[] specifications)
        {
            _specifications = specifications ?? throw new ArgumentNullException(nameof(specifications));
        }

        public AndSpecification(IEnumerable<ISpecification<T>> specifications)
        {
            _specifications = specifications?.ToList() ?? throw new ArgumentNullException(nameof(specifications));
        }

        public bool IsSatisfiedBy(T entity)
        {
            return _specifications.All(spec => spec.IsSatisfiedBy(entity));
        }

        public IQueryable<T> Apply(IQueryable<T> query)
        {
            return _specifications.Aggregate(query, (current, spec) => spec.Apply(current));
        }
    }
}
