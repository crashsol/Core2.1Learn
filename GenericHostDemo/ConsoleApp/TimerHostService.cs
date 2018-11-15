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

    public class TimerHostService : BackgroundService
    {
        private Timer _timer;

        private readonly AppSettings _appSettings;
        private readonly ILogger<TimerHostService> _logger;

        public TimerHostService(IOptions<AppSettings> options,ILogger<TimerHostService> logger)
        {
            _appSettings = options.Value;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //启动timer
            _logger.LogInformation("Timer Is Start");
            _timer = new Timer((obj) => _logger.LogInformation("Timer Is Running"), null, TimeSpan.Zero,TimeSpan.FromSeconds(_appSettings.PrinterDelaySecond));
            return Task.CompletedTask;          
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timer is Stopping");
            _timer?.Change(Timeout.Infinite, 0);
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}
