using NasaDataset.Application.Common.Interfaces;
using NasaDataset.Application.Meteorites.Dtos;
using NasaDataset.Application.Meteorites.Interfaces;
using NasaDataset.Application.Meteorites.Mappers;
using NasaDataset.Domain.ValueObjects;

namespace NasaDataset.Application.Meteorites.Services
{
    public class MeteoriteSyncHandler : IMeteoriteSyncHandler
    {

        private readonly IMeteoriteSyncService _syncService;
        private readonly IMeteoriteRepository _repository;
        private readonly ICacheService _cacheService;


        public MeteoriteSyncHandler(IMeteoriteSyncService syncService, IMeteoriteRepository repository, ICacheService cacheService)
        {
            _syncService = syncService;
            _repository = repository;
            _cacheService = cacheService;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var externalData = await FetchExternalDataAsync(cancellationToken).ConfigureAwait(false);
            var idsFromDatabase = await _repository.GetExternalIdsAsync(cancellationToken).ConfigureAwait(false);
            var externalMeteoritesWithIds = ParseExternalMeteorites(externalData);

            var idsFromExternalData = externalMeteoritesWithIds.Select(x => x.Id).ToList();

            var idsToAdd = GetMissingItems(idsFromExternalData, idsFromDatabase);
            var idsToRemove = GetMissingItems(idsFromDatabase, idsFromExternalData);

            await AddNewMeteoritesAsync(externalMeteoritesWithIds, idsToAdd, cancellationToken).ConfigureAwait(false);
            await _repository.DeleteRangeAsync(idsToRemove, cancellationToken).ConfigureAwait(false);

            if(idsToAdd.Any() || idsToRemove.Any())
                _cacheService.ClearMeteoriteCache();
        }

        private List<T> GetMissingItems<T>(
            IEnumerable<T> sourceList,
            IEnumerable<T> compareList)
        {
            var compareKeys = new HashSet<T>(compareList);
            return sourceList.Where(item => !compareKeys.Contains(item)).ToList();
        }

        private async Task<IEnumerable<CreateMeteoriteDto>> FetchExternalDataAsync(CancellationToken ct) =>
            await _syncService.FetchMeteoriteDataAsync(ct);

        private List<(CreateMeteoriteDto Dto, ExternalMeteoriteId Id)> ParseExternalMeteorites(IEnumerable<CreateMeteoriteDto> dtos) =>
            dtos.Select(dto => (dto, new ExternalMeteoriteId(int.Parse(dto.ExternalId)))).ToList();

        private async Task AddNewMeteoritesAsync(IEnumerable<(CreateMeteoriteDto Dto, ExternalMeteoriteId Id)> parsed, IEnumerable<ExternalMeteoriteId> idsToAdd, CancellationToken ct)
        {
            var toAdd = parsed
                .Where(p => idsToAdd.Contains(p.Id))
                .Select(p => p.Dto.ToEntity())
                .ToList();

            if (toAdd.Any())
                await _repository.AddRangeAsync(toAdd, ct).ConfigureAwait(false);
        }
    }
}