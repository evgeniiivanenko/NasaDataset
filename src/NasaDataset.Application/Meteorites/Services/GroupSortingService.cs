using NasaDataset.Application.Common.Interfaces;
using NasaDataset.Application.Common.Strategies;
using NasaDataset.Application.Meteorites.Dtos;
using NasaDataset.Application.Meteorites.Interfaces;

namespace NasaDataset.Application.Meteorites.Services
{
    public class GroupSortingService : IGroupSortingService
    {
        private readonly Dictionary<string, ISortingStrategy<MeteoriteGroupResultDto>> _strategies;

        public GroupSortingService()
        {
            _strategies = new Dictionary<string, ISortingStrategy<MeteoriteGroupResultDto>>(StringComparer.OrdinalIgnoreCase)
            {
                ["key"] = new FieldSortingStrategy<MeteoriteGroupResultDto, string>(m => m.Key),
                ["count"] = new FieldSortingStrategy<MeteoriteGroupResultDto, int>(m => m.Count),
                ["totalMass"] = new FieldSortingStrategy<MeteoriteGroupResultDto, decimal>(m => m.TotalMass),
            };
        }

        public ISortingStrategy<MeteoriteGroupResultDto> ResolveStrategy(string sortBy, string direction)
        {
            if (_strategies.TryGetValue(sortBy, out var strategy))
            {
                strategy.Direction = direction;
                return strategy;
            }

            return _strategies["key"];
        }
    }
}
