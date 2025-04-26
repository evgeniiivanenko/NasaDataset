using NasaDataset.Application.Common.Models;

namespace NasaDataset.Application.Meteorites.Requests.GetMeteorites
{
    public class GetMeteoritesRequest : PaginationAndSortingBase
    {
        public string? SearchTerm { get; set; }

        public string? Recclass { get; set; }
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }

    }
}
