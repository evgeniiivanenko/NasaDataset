namespace NasaDataset.Application.Common.Interfaces
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);
        IQueryable<T> Apply(IQueryable<T> query);
    }
}
