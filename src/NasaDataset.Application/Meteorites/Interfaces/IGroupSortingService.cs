using NasaDataset.Application.Common.Interfaces;
using NasaDataset.Application.Meteorites.Dtos;

namespace NasaDataset.Application.Meteorites.Interfaces
{
    public interface IGroupSortingService
    {
        ISortingStrategy<MeteoriteGroupResultDto> ResolveStrategy(string sortBy, string direction);
    }
}
