using Microsoft.Extensions.Options;
using NasaDataset.Application.Meteorites.Dtos;
using NasaDataset.Application.Meteorites.Interfaces;
using NasaDataset.Infrastructure.Configuration;
using System.Net.Http.Json;

namespace NasaDataset.Infrastructure.Services
{
    public class MeteoriteSyncService : IMeteoriteSyncService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _urlForRequest;

        public MeteoriteSyncService(IHttpClientFactory httpClientFactory, IOptions<MeteoriteSyncFromUrlSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _urlForRequest = options.Value.Url;
        }

        public async Task<IEnumerable<CreateMeteoriteDto>> FetchMeteoriteDataAsync(CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(_urlForRequest))
                throw new ArgumentNullException(nameof(_urlForRequest));

            var client = _httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<List<CreateMeteoriteDto>>(_urlForRequest, cancellationToken);

            return response ?? new();
        }
    }
}
