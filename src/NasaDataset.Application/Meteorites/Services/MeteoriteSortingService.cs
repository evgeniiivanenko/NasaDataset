using NasaDataset.Application.Common.Interfaces;
using NasaDataset.Application.Common.Strategies;
using NasaDataset.Application.Meteorites.Interfaces;
using NasaDataset.Domain.Entities;
using NasaDataset.Domain.Enums;
using NasaDataset.Domain.ValueObjects;

namespace NasaDataset.Application.Meteorites.Services
{
    public class MeteoriteSortingService : IMeteoriteSortingService
    {
        private readonly Dictionary<string, ISortingStrategy<Meteorite>> _strategies;

        public MeteoriteSortingService()
        {
            _strategies = new Dictionary<string, ISortingStrategy<Meteorite>>(StringComparer.OrdinalIgnoreCase)
            {
                ["id"] = new FieldSortingStrategy<Meteorite, ExternalMeteoriteId>(m => m.ExternalId),
                ["name"] = new FieldSortingStrategy<Meteorite, string>(m => m.Name!),
                ["nameType"] = new FieldSortingStrategy<Meteorite, NameType>(m => m.NameType),
                ["recclass"] = new FieldSortingStrategy<Meteorite, string>(m => m.Recclass!),
                ["mass"] = new FieldSortingStrategy<Meteorite, decimal>(m => m.Mass),
                ["fall"] = new FieldSortingStrategy<Meteorite, FallType>(m => m.Fall),
                ["year"] = new FieldSortingStrategy<Meteorite, int?>(m => m.Year),
                ["reclat"] = new FieldSortingStrategy<Meteorite, Reclat>(m => m.Reclat),
                ["reclong"] = new FieldSortingStrategy<Meteorite, Reclong>(m => m.Reclong),
            };
        }

        public ISortingStrategy<Meteorite> ResolveStrategy(string sortBy, string direction)
        {
            if (_strategies.TryGetValue(sortBy, out var strategy))
            {
                strategy.Direction = direction;
                return strategy;
            }

            return _strategies["id"];
        }

    }
}
