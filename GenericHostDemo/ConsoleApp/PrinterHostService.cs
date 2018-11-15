using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class PrinterHostService : BackgroundService
    {
        private readonly ILogger<PrinterHostService> _logger;

        private readonly AppSettings _settings;

        public PrinterHostService(ILogger<PrinterHostService> logger,IOptions<AppSettings> options)
        {
            _logger = logger;
            _settings = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if(DateTime.Now.Minute> 1)
                {
                    break;
                }
                _logger.LogInformation($"Printer Service Running");
                await Task.Delay(TimeSpan.FromSeconds(_settings.PrinterDelaySecond));
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogError("Printer Sevice Stopping");
            return base.StopAsync(cancellationToken);
        }
    }
}
