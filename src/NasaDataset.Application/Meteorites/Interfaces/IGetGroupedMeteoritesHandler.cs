using NasaDataset.Application.Common.Models;
using NasaDataset.Application.Meteorites.Dtos;
using NasaDataset.Application.Meteorites.Requests.GetGroupedMeteorites;

namespace NasaDataset.Application.Meteorites.Interfaces
{
    public interface IGetGroupedMeteoritesHandler
    {
        Task<PaginatedList<MeteoriteGroupResultDto>> HandleAsync(GetGroupMeteoritRequest request, CancellationToken cancellationToken);
    }
}
