using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SyncService
{
    public class ImportWorker : BackgroundService
    {
        private readonly ILogger<ImportWorker> _logger;
        IRepository<EMail> repository;

        public ImportWorker(ILogger<ImportWorker> logger, IRepository<EMail> repo)
        {
            _logger = logger;
            this.repository = repo;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Importworker running at: {time}", DateTimeOffset.Now);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
