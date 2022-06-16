namespace WorkerService1
{
    public class Worker1 : BackgroundService
    {
        private readonly ILogger<Worker1> _logger;
        private readonly MyDataClass status;

        public Worker1(ILogger<Worker1> logger, MyDataClass status)
        {
            _logger = logger;
            this.status = status;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //Test anything that needs to be tested
                status.MyStatus = status.MyStatus + 1;
                status.MyStatus2 = status.MyStatus2 + 1;
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
    public class Worker2 : BackgroundService
    {
        private readonly ILogger<Worker2> _logger;
        private readonly MyDataClass status;

        public Worker2(ILogger<Worker2> logger, MyDataClass status)
        {
            _logger = logger;
            this.status = status;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            status.MyStatusChanged += Status_MyStatusChanged;
            status.MyStatus2Changed += Status_MyStatus2Changed;
            return base.StartAsync(cancellationToken);
        }

        private void Status_MyStatus2Changed(int s)
        {
            _logger.LogInformation("Status2 has changed to {status}", s);
        }

        private void Status_MyStatusChanged(int s)
        {
            _logger.LogInformation("Status has changed to {status}", s);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker2 running at: {time}", DateTimeOffset.Now);
                //Test anything that needs to be tested
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            status.MyStatusChanged -= Status_MyStatusChanged;
            status.MyStatus2Changed -= Status_MyStatus2Changed;
            return base.StopAsync(cancellationToken);
        }
    }
}