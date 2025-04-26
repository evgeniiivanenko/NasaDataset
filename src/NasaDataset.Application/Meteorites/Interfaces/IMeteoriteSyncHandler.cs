namespace NasaDataset.Application.Meteorites.Interfaces
{
    public interface IMeteoriteSyncHandler
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
