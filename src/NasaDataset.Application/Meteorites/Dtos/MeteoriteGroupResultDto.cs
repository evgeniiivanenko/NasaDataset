namespace NasaDataset.Application.Meteorites.Dtos
{
    public class MeteoriteGroupResultDto
    {
        public string Key { get; set; } = default!;
        
        public int Count { get; set; }

        public decimal TotalMass { get; set; }
    }
}
