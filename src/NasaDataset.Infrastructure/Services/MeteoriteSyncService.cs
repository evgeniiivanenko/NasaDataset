using NasaDataset.Application.Meteorites.Dtos;
using NasaDataset.Application.Meteorites.Interfaces;
using NasaDataset.Application.Meteorites.Mappers;
using System.Net.Http.Json;
using System.Text.Json;

namespace NasaDataset.Infrastructure.Services
{
    public class MeteoriteSyncService : IMeteoriteSyncService
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public MeteoriteSyncService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<CreateMeteoriteDto>> FetchMeteoriteDataAsync(CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient("meteorite");

            var response = await client.GetFromJsonAsync<List<CreateMeteoriteDto>>("https://raw.githubusercontent.com/biggiko/nasa-dataset/refs/heads/main/y77d-th95.json", cancellationToken);

            return response ?? new();
        }
    }
}
