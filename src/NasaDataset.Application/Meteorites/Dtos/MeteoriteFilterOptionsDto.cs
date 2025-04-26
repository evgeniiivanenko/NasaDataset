namespace NasaDataset.Application.Meteorites.Dtos
{
    public class MeteoriteFilterOptionsDto
    {
        public List<string> Recclasses { get; set; } = new();
        public List<int> Years { get; set; } = new();
    }
}
