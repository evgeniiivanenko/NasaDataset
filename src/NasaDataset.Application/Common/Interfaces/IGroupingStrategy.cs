namespace NasaDataset.Application.Common.Interfaces
{
    public interface IGroupingStrategy<T, TResult>
    {
        IQueryable<TResult> Group(IQueryable<T> query);
    }
}
