using NasaDataset.Application.Common.Models;
using NasaDataset.Application.Meteorites.Dtos;
using NasaDataset.Application.Meteorites.Requests.GetMeteorites;

namespace NasaDataset.Application.Meteorites.Interfaces
{
    public interface IGetMeteoritesHandler
    {
        Task<PaginatedList<GetMeteoriteDto>> HandleAsync(GetMeteoritesRequest request, CancellationToken cancellationToken);
    }
}
