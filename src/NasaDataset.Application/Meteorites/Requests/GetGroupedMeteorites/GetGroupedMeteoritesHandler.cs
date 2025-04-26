using FluentValidation;
using NasaDataset.Application.Common.Interfaces;
using NasaDataset.Application.Common.Models;
using NasaDataset.Application.Meteorites.Dtos;
using NasaDataset.Application.Meteorites.Interfaces;
using NasaDataset.Application.Meteorites.Specifications;
using NasaDataset.Application.Meteorites.Strategies;
using NasaDataset.Domain.Entities;

namespace NasaDataset.Application.Meteorites.Requests.GetGroupedMeteorites
{
    public class GetGroupedMeteoritesHandler : IGetGroupedMeteoritesHandler
    {
        private readonly IMeteoriteRepository _repository;
        private readonly IGroupSortingService _sortingService;
        private readonly IValidator<PaginationAndSortingBase> _validator;


        public GetGroupedMeteoritesHandler(IMeteoriteRepository repository, IGroupSortingService sortingService, IValidator<PaginationAndSortingBase> validator)
        {
            _repository = repository;
            _sortingService = sortingService;
            _validator = validator;
        }

        public async Task<PaginatedList<MeteoriteGroupResultDto>> HandleAsync(GetGroupMeteoritRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            ISpecification<Meteorite> spec = new YearRangeSpecification(request.YearFrom, request.YearTo);

            var sortingStrategy = _sortingService.ResolveStrategy(request.SortBy!, request.Direction);

            IGroupingStrategy<Meteorite, MeteoriteGroupResultDto> groupingStratery = new YearGroupingStrategy();

            var result = await _repository.GetGroupedAsync(spec, sortingStrategy, groupingStratery, request.PageNumber, request.PageSize, cancellationToken);

            return result;
        }
    }
}
