using NasaDataset.Domain.Enums;
using NasaDataset.Domain.ValueObjects;

namespace NasaDataset.Domain.Entities
{
    public class Meteorite
    {
        public Guid Id { get; init; }

        public ExternalMeteoriteId ExternalId { get; init; }

        public string? Name { get; init; }

        public NameType NameType { get; init; }

        public string Recclass { get; init; } = string.Empty;

        public decimal Mass { get; init; }

        public FallType Fall { get; init; }

        public int? Year { get; init; }

        public Reclat Reclat { get; init; }

        public Reclong Reclong { get; init; }

        public Geolocation? Geolocation { get; init; }

        public string? ComputedRegionCbhkFwbd { get; init; }

        public string? ComputedRegionNnqa25f4 { get; init; }

        public Meteorite() { }

        public Meteorite(ExternalMeteoriteId externalId, string name, NameType nameType, string recclass,
            Mass mass, FallType fall, int? year, Reclat reclat, Reclong reclong, Geolocation geolocation,
            string? computedRegionCbhkFwbd = null, string? computedRegionNnqa25f4 = null)
        {
            ExternalId = externalId;
            Name = name;
            NameType = nameType;
            Recclass = recclass;
            Mass = mass.Value;
            Fall = fall;
            Year = year;
            Reclat = reclat;
            Reclong = reclong;
            Geolocation = geolocation;
            ComputedRegionCbhkFwbd = computedRegionCbhkFwbd;
            ComputedRegionNnqa25f4 = computedRegionNnqa25f4;
        }

    }
}
