using NasaDataset.Application.Common.Models;

namespace NasaDataset.Application.Meteorites.Requests.GetGroupedMeteorites
{
    public class GetGroupMeteoritRequest : PaginationAndSortingBase
    {
        public string Key { get; set; } = default!;
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }
    }
}
