namespace NasaDataset.Application.Common.Models
{
    public class PaginationAndSortingBase
    {
        public string? SortBy { get; set; } = string.Empty;
        public string Direction { get; set; } = "asc";

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
