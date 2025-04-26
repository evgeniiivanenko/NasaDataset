using Microsoft.EntityFrameworkCore;
using NasaDataset.Application.Common.Interfaces;
using NasaDataset.Domain.Entities;
using NasaDataset.Domain.ValueObjects;

namespace NasaDataset.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Meteorite> Meteorites => Set<Meteorite>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
