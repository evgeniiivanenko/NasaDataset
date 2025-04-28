using NasaDataset.Application.Meteorites.Dtos;
using NasaDataset.Application.Meteorites.Interfaces;
using System.Text.Json;

namespace NasaDataset.Infrastructure.Services
{
    public class MeteoriteSyncFromFileService : IMeteoriteSyncService
    {
        public MeteoriteSyncFromFileService()
        {
            
        }

        public async Task<IEnumerable<CreateMeteoriteDto>> FetchMeteoriteDataAsync(CancellationToken cancellationToken)
        {
            using (FileStream fs = File.OpenRead("nasa-dataset.json"))
            {
                var dataset = await JsonSerializer.DeserializeAsync<List<CreateMeteoriteDto>>(fs);
                return dataset ?? new List<CreateMeteoriteDto>();
            }
        }
    }
}
