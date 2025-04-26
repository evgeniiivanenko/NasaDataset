using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NasaDataset.Domain.Entities;
using NasaDataset.Domain.Enums;
using NasaDataset.Domain.ValueObjects;
using System.Reflection.Emit;
using System.Text.Json;

namespace NasaDataset.Infrastructure.Data.Configurations
{
    public class MeteoriteConfiguration : IEntityTypeConfiguration<Meteorite>
    {
        public void Configure(EntityTypeBuilder<Meteorite> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(m => m.ExternalId)
                .HasConversion(e => e.Id, e => new ExternalMeteoriteId(e));

            builder.HasIndex(m => m.ExternalId).IsUnique();

            builder.Property(m => m.Reclat)
                .HasConversion(m => m.Value, v => new Reclat(v));

            builder.Property(m => m.Reclong)
                .HasConversion(m => m.Value, v => new Reclong(v));

            builder.Property(m => m.NameType)
                .HasConversion(
                v => v.ToString(),
                v => (NameType)Enum.Parse(typeof(NameType), v));

            builder.Property(m => m.Fall)
                .HasConversion(
                v => v.ToString(),
                v => (FallType)Enum.Parse(typeof(FallType), v));

            builder.HasIndex(m => m.Year);

            builder.Property(m => m.Geolocation)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<Geolocation>(v, (JsonSerializerOptions)null))
                .HasColumnType("jsonb")
                .IsRequired(false);
        }
    }
}
