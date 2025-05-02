using Microsoft.Extensions.Options;
using NasaDataset.Application.Meteorites.Dtos;
using NasaDataset.Application.Meteorites.Interfaces;
using NasaDataset.Infrastructure.Configuration;
using System.Text.Json;

namespace NasaDataset.Infrastructure.Services
{
    public class MeteoriteSyncFromFileService : IMeteoriteSyncService
    {

        private readonly string _filePath;

        public MeteoriteSyncFromFileService(IOptions<MeteoriteSyncFromFileSettings> options)
        {
            _filePath = options.Value.FilePath;
        }

        public async Task<IEnumerable<CreateMeteoriteDto>> FetchMeteoriteDataAsync(CancellationToken cancellationToken)
        {

            if (File.Exists(_filePath))
            {
                using (FileStream fs = File.OpenRead(_filePath))
                {
                    var dataset = await JsonSerializer.DeserializeAsync<List<CreateMeteoriteDto>>(fs);
                    return dataset ?? new List<CreateMeteoriteDto>();
                }
            }
            else
            {
                throw new FileNotFoundException(_filePath);
            }
        }
    }
}
