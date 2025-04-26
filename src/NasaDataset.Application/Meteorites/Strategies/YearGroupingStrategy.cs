using NasaDataset.Application.Common.Interfaces;
using NasaDataset.Application.Meteorites.Dtos;
using NasaDataset.Domain.Entities;

namespace NasaDataset.Application.Meteorites.Strategies
{
    public class YearGroupingStrategy : IGroupingStrategy<Meteorite, MeteoriteGroupResultDto>
    {
        public IQueryable<MeteoriteGroupResultDto> Group(IQueryable<Meteorite> query) =>
            query
            .GroupBy(m => m.Year)
                .Select(g => new MeteoriteGroupResultDto
                {
                    Key = g.Key.HasValue ? g.Key.Value.ToString() : "0",
                    Count = g.Count(),
                    TotalMass = g.Sum(m => m.Mass)
                });
    }
}
