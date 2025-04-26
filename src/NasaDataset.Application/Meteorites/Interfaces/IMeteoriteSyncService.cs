using NasaDataset.Application.Meteorites.Dtos;

namespace NasaDataset.Application.Meteorites.Interfaces
{
    public interface IMeteoriteSyncService
    {
        Task<IEnumerable<CreateMeteoriteDto>> FetchMeteoriteDataAsync(CancellationToken cancellationToken);
    }
}
