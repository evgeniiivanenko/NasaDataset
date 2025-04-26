using FluentValidation;

namespace NasaDataset.Application.Common.Models
{
    public class PaginationAndSortingBaseValidator : AbstractValidator<PaginationAndSortingBase>
    {
        public PaginationAndSortingBaseValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("PageNumber must be greater than 0.");

            RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("PageSize must be between 1 and 100.");

            RuleFor(x => x.Direction)
            .Must(d => d == null || d.Equals("asc", StringComparison.OrdinalIgnoreCase) || d.Equals("desc", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Direction must be either 'asc' or 'desc'.");
        }
    }
}
