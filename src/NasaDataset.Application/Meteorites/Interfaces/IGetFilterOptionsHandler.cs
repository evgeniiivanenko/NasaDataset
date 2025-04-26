using NasaDataset.Application.Meteorites.Dtos;

namespace NasaDataset.Application.Meteorites.Interfaces
{
    public interface IGetFilterOptionsHandler
    {
        Task<MeteoriteFilterOptionsDto> HandleAsync(CancellationToken cancellationToken);
    }
}
