using Microsoft.EntityFrameworkCore;
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
        private readonly IApplicationDbContext _context;


        public MeteoriteSyncHandler(IMeteoriteSyncService syncService, IApplicationDbContext context)
        {
            _syncService = syncService;
            _context = context;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var externalData = await FetchExternalDataAsync(cancellationToken).ConfigureAwait(false);
            var idsFromDatabase = await GetDatabaseExternalIdsAsync(cancellationToken).ConfigureAwait(false);
            var externalMeteoritesWithIds = ParseExternalMeteorites(externalData);

            var idsFromExternalData = externalMeteoritesWithIds.Select(x => x.Id).ToList();

            var idsToAdd = GetMissingItems(idsFromExternalData, idsFromDatabase);
            var idsToRemove = GetMissingItems(idsFromDatabase, idsFromExternalData);

            await AddNewMeteoritesAsync(externalMeteoritesWithIds, idsToAdd, cancellationToken).ConfigureAwait(false);
            await RemoveDeletedMeteoritesAsync(idsToRemove, cancellationToken).ConfigureAwait(false);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
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

        private async Task<List<ExternalMeteoriteId>> GetDatabaseExternalIdsAsync(CancellationToken ct) =>
            await _context.Meteorites.Select(x => x.ExternalId).ToListAsync(ct);

        private List<(CreateMeteoriteDto Dto, ExternalMeteoriteId Id)> ParseExternalMeteorites(IEnumerable<CreateMeteoriteDto> dtos) =>
            dtos.Select(dto => (dto, new ExternalMeteoriteId(int.Parse(dto.ExternalId)))).ToList();

        private async Task AddNewMeteoritesAsync(IEnumerable<(CreateMeteoriteDto Dto, ExternalMeteoriteId Id)> parsed, IEnumerable<ExternalMeteoriteId> idsToAdd, CancellationToken ct)
        {
            var toAdd = parsed
                .Where(p => idsToAdd.Contains(p.Id))
                .Select(p => p.Dto.ToEntity())
                .ToList();

            if (toAdd.Any())
                await _context.Meteorites.AddRangeAsync(toAdd, ct).ConfigureAwait(false);
        }

        private async Task RemoveDeletedMeteoritesAsync(IEnumerable<ExternalMeteoriteId> idsToRemove, CancellationToken ct)
        {
            if (!idsToRemove.Any()) return;

            var toRemove = await _context.Meteorites
                .Where(m => idsToRemove.Contains(m.ExternalId))
                .ToListAsync(ct)
                .ConfigureAwait(false);

            _context.Meteorites.RemoveRange(toRemove);
        }
    }
}
