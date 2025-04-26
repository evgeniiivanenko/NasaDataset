using System.Text.Json.Serialization;

namespace NasaDataset.Application.Meteorites.Dtos
{
    public class GeolocationDto
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = default!;

        [JsonPropertyName("coordinates")]
        public List<double> Coordinates { get; set; } = new();
    }
}
