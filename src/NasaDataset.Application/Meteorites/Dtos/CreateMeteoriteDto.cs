using System.Text.Json.Serialization;

namespace NasaDataset.Application.Meteorites.Dtos
{
    public class CreateMeteoriteDto
    {
        [JsonPropertyName("id")]
        public required string ExternalId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("nametype")]
        public string Nametype { get; set; } = default!;

        [JsonPropertyName("recclass")]
        public string Recclass { get; set; } = default!;

        [JsonPropertyName("mass")]
        public string Mass { get; set; } = default!;

        [JsonPropertyName("fall")]
        public string Fall { get; set; } = default!;

        [JsonPropertyName("year")]
        public string? Year { get; set; }

        [JsonPropertyName("reclat")]
        public string Reclat { get; set; } = default!;

        [JsonPropertyName("reclong")]
        public string Reclong { get; set; } = default!;

        [JsonPropertyName("geolocation")]
        public GeolocationDto? Geolocation { get; set; }

        [JsonPropertyName(":@computed_region_cbhk_fwbd")]
        public string? ComputedRegionCbhkFwbd { get; set; }

        [JsonPropertyName(":@computed_region_nnqa_25f4")]
        public string? ComputedRegionNnqa25f4 { get; set; }
    }
}
