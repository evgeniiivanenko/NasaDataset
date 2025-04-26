using NasaDataset.Application.Meteorites.Dtos;
using NasaDataset.Domain.Entities;
using NasaDataset.Domain.Enums;
using NasaDataset.Domain.ValueObjects;

namespace NasaDataset.Application.Meteorites.Mappers
{
    public static class MeteoriteMapper
    {
        public static Meteorite ToEntity(this CreateMeteoriteDto dto)
        {
            return new Meteorite
            (
                new ExternalMeteoriteId(int.TryParse(dto.ExternalId, out int externalId) ? externalId : throw new ArgumentException("id")),
                dto.Name,
                (NameType)Enum.Parse(typeof(NameType), dto.Nametype),
                dto.Recclass,
                Mass.FromString(dto.Mass),
                (FallType)Enum.Parse(typeof(FallType), dto.Fall),
                DateTime.TryParse(dto.Year, out DateTime year) ? year.Year : null,
                Reclat.FromString(dto.Reclat),
                Reclong.FromString(dto.Reclong),
                new Geolocation(dto.Geolocation?.Type ?? "", dto.Geolocation?.Coordinates[0] ?? 0, dto.Geolocation?.Coordinates[1] ?? 0),
                dto.ComputedRegionCbhkFwbd,
                dto.ComputedRegionNnqa25f4
            );

        }

        public static GetMeteoriteDto ToDto(this Meteorite meteorite)
        {
            return new GetMeteoriteDto
            {
                ExternalId = meteorite.ExternalId.Id,
                Name = meteorite.Name,
                Nametype = meteorite.NameType.ToString(),
                Recclass = meteorite.Recclass,
                Mass = meteorite.Mass.ToString(),
                Fall = meteorite.Fall.ToString(),
                Year = meteorite.Year,
                Reclat = (double)meteorite.Reclat.Value,
                Reclong = (double)meteorite.Reclong.Value,
                Geolocation = meteorite.Geolocation is not null ?
                    new GeolocationDto
                    {
                        Type = meteorite.Geolocation?.Type ?? "",
                        Coordinates = new List<double> { meteorite.Geolocation!.Longitude, meteorite.Geolocation!.Latitude }
                    }
                    : null,
                ComputedRegionCbhkFwbd = meteorite.ComputedRegionCbhkFwbd,
                ComputedRegionNnqa25f4 = meteorite.ComputedRegionNnqa25f4
            };
        }
    }
}
