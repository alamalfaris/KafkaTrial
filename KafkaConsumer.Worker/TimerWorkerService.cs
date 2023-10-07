using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.Worker
{
    internal class TimerWorkerService : IHostedService, IDisposable
    {
        private readonly ILogger<TimerWorkerService> _logger;
        private Timer _timer;

        public TimerWorkerService(ILogger<TimerWorkerService> logger)
        {
            _logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(OnTimer, cancellationToken, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void OnTimer(object state)
        {
            _logger.LogInformation("OnTimer event called");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
