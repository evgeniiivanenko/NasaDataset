using NasaDataset.Application.Common.Interfaces;
using NasaDataset.Domain.Entities;

namespace NasaDataset.Application.Meteorites.Interfaces
{
    public interface IMeteoriteSortingService
    {
        ISortingStrategy<Meteorite> ResolveStrategy(string sortBy, string direction);
    }
}
