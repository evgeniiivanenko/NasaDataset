using NasaDataset.Application.Common.Interfaces;
using NasaDataset.Application.Meteorites.Dtos;
using NasaDataset.Application.Meteorites.Interfaces;

namespace NasaDataset.Application.Meteorites.Requests.GetFilterOptions
{
    public class GetFilterOptionsHandler : IGetFilterOptionsHandler
    {
        private const string FILTERS_CACHE_KEY = "MeteoriteFilterOptions";
        private readonly IMeteoriteRepository _repository;
        private readonly ICacheService _cacheService;

        public GetFilterOptionsHandler(IMeteoriteRepository repository, ICacheService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }

        public async Task<MeteoriteFilterOptionsDto> HandleAsync(CancellationToken cancellationToken)
        {
            if (_cacheService.Get<MeteoriteFilterOptionsDto>(FILTERS_CACHE_KEY) is { } cached)
                return cached;

            var recclasses = await _repository.GetDistinctRecclassesAsync(cancellationToken);
            var years = await _repository.GetDistinctYearsAsync(cancellationToken);

            var options = new MeteoriteFilterOptionsDto
            {
                Recclasses = recclasses,
                Years = years
            };

            _cacheService.Set(FILTERS_CACHE_KEY, options, TimeSpan.FromHours(1));

            return options;
        }
    }
}
