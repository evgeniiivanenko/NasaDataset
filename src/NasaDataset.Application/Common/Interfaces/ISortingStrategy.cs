namespace NasaDataset.Application.Common.Interfaces
{
    public interface ISortingStrategy<T>
    {
        string Direction { get; set; }
        IQueryable<T> ApplySort(IQueryable<T> query);
    }
}
