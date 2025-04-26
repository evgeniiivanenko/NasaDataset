namespace NasaDataset.Domain.ValueObjects
{
    public record Reclong(decimal Value)
    {
        public static Reclong FromString(string reclongString)
            => new(decimal.TryParse(reclongString, out var reclong) ? reclong : 0);
    }
}
