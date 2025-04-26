using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NasaDataset.Application.Common.Interfaces;
using NasaDataset.Application.Common.Models;
using NasaDataset.Application.Common.Services;
using NasaDataset.Application.Meteorites.Interfaces;
using NasaDataset.Application.Meteorites.Requests.GetFilterOptions;
using NasaDataset.Application.Meteorites.Requests.GetGroupedMeteorites;
using NasaDataset.Application.Meteorites.Requests.GetMeteorites;
using NasaDataset.Application.Meteorites.Services;

namespace NasaDataset.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IMeteoriteSyncHandler, MeteoriteSyncHandler>();
            builder.Services.AddScoped<IGetFilterOptionsHandler, GetFilterOptionsHandler>();
            builder.Services.AddScoped<IGetMeteoritesHandler, GetMeteoritesHandler>();
            builder.Services.AddScoped<IGetGroupedMeteoritesHandler, GetGroupedMeteoritesHandler>();
            builder.Services.AddScoped<ICacheService, CacheService>();
            builder.Services.AddSingleton<IMeteoriteSortingService, MeteoriteSortingService>();
            builder.Services.AddSingleton<IGroupSortingService, GroupSortingService>();

            builder.Services.AddValidatorsFromAssemblyContaining<PaginationAndSortingBaseValidator>();
        }
    }
}
