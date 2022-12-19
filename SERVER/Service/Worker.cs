using ESMP.STOCK.API.QUERYAPI;
using SERVER;
using System.Text;

namespace Service
{
    public class Worker : BackgroundService
    {
        //private readonly ILogger<Worker> _logger;
        private readonly IConfiguration? Configuration;
        private ServerMode? serverMode;
        private BaseAPI? _realizedProfitAndLoss;
        private string? _connstr;

        public Worker(IConfiguration configuration)
        {
            //_logger = logger;
            Configuration = configuration;
            _connstr = Configuration.GetValue<string>("ConnectionString");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Init();
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
        public void Init()
        {
            //Debug.WriteLine("MainModel °õ¦æÄò½X: " + Thread.CurrentThread.ManagedThreadId);
            _realizedProfitAndLoss = new BaseAPI(_connstr);
            serverMode = new ServerMode(Encoding.UTF8);
            serverMode._receiveEvent += ReceiveEvent;
        }

        private void ReceiveEvent(ServerMode clientAsync, ReceiveEventArgs e)
        {
            serverMode.SocketSend(_realizedProfitAndLoss.Receiver(e.resoult));
        }
    }
}