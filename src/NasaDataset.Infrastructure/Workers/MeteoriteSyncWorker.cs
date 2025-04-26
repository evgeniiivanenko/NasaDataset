using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NasaDataset.Application.Meteorites.Interfaces;
using NasaDataset.Infrastructure.Configuration;

namespace NasaDataset.Infrastructure.Workers
{
    public class MeteoriteSyncWorker : BackgroundService
    {

        private readonly ILogger<MeteoriteSyncWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval;

        public MeteoriteSyncWorker(ILogger<MeteoriteSyncWorker> logger, IServiceProvider serviceProvider, IOptions<WorkerSettings> options)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _interval = TimeSpan.FromMinutes(options.Value.SyncIntervalMinutes);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("MeteoriteSyncWorker запущен");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var syncHandler = scope.ServiceProvider.GetRequiredService<IMeteoriteSyncHandler>();

                    _logger.LogInformation("Начинаем синхронизацию метеоритов");
                    await syncHandler.ExecuteAsync(stoppingToken);
                    _logger.LogInformation("Синхронизация завершена");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при синхронизации данных метеоритов");
                }

                // Ждём до следующего запуска
                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}
