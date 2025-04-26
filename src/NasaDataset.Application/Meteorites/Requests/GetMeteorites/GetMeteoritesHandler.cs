using FluentValidation;
using NasaDataset.Application.Common.Interfaces;
using NasaDataset.Application.Common.Models;
using NasaDataset.Application.Meteorites.Dtos;
using NasaDataset.Application.Meteorites.Interfaces;
using NasaDataset.Application.Meteorites.Mappers;
using NasaDataset.Application.Meteorites.Specifications;
using NasaDataset.Domain.Entities;

namespace NasaDataset.Application.Meteorites.Requests.GetMeteorites
{
    public class GetMeteoritesHandler : IGetMeteoritesHandler
    {
        private readonly IMeteoriteRepository _repository;
        private readonly IMeteoriteSortingService _sortingService;
        private readonly IValidator<PaginationAndSortingBase> _validator;

        public GetMeteoritesHandler(IMeteoriteRepository repository, IMeteoriteSortingService sortingService, IValidator<PaginationAndSortingBase> validator)
        {
            _repository = repository;
            _sortingService = sortingService;
            _validator = validator;
        }

        public async Task<PaginatedList<GetMeteoriteDto>> HandleAsync(GetMeteoritesRequest request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var spec = new AndSpecification<Meteorite>(
                        new NameSearchSpecification(request.SearchTerm!),
                        new YearRangeSpecification(request.YearFrom, request.YearTo),
                        new RecclassSpecification(request.Recclass!)
                    );

            var sortingStrategy = _sortingService.ResolveStrategy(request.SortBy!, request.Direction);

            var result = await _repository.GetFilteredAsync(spec, sortingStrategy, (item) => item.ToDto(), request.PageNumber, request.PageSize, cancellationToken);

            return result;
        }
    }
}
