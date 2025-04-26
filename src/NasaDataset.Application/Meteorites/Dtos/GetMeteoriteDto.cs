namespace NasaDataset.Application.Meteorites.Dtos
{
    public class GetMeteoriteDto
    {
        public int ExternalId { get; set; }
        public string? Name { get; set; } = default!;
        public string? Nametype { get; set; } = default!;
        public string? Recclass { get; set; } = default!;
        public string? Mass { get; set; } = default!;
        public string? Fall { get; set; } = default!;
        public int? Year { get; set; }
        public double Reclat { get; set; } = default!;
        public double Reclong { get; set; } = default!;
        public GeolocationDto? Geolocation { get; set; }
        public string? ComputedRegionCbhkFwbd { get; set; }
        public string? ComputedRegionNnqa25f4 { get; set; }
    }
}
