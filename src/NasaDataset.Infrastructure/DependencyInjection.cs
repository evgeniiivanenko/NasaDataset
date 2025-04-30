using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NasaDataset.Application.Common.Interfaces;
using NasaDataset.Application.Meteorites.Interfaces;
using NasaDataset.Application.Meteorites.Services;
using NasaDataset.Infrastructure.Configuration;
using NasaDataset.Infrastructure.Data;
using NasaDataset.Infrastructure.Repositories;
using NasaDataset.Infrastructure.Services;
using NasaDataset.Infrastructure.Workers;
using Npgsql;
using System.Data;

namespace NasaDataset.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("NasaDatasetDb");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            builder.Services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(connectionString));

            builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            builder.Services.AddScoped<ApplicationDbContextInitialiser>();
            builder.Services.AddScoped<IMeteoriteSyncService, MeteoriteSyncFromFileService>();
            builder.Services.AddScoped<IMeteoriteRepository, MeteoriteRepository>();
            builder.Services.AddHostedService<MeteoriteSyncWorker>();

        }
    }
}
