namespace NasaDataset.Domain.ValueObjects
{
    public record Reclat(decimal Value)
    {
        public static Reclat FromString(string reclatString)
            => new(decimal.TryParse(reclatString, out var reclat) ? reclat : 0);
    }
}
