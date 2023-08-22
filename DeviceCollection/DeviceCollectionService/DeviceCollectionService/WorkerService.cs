using DeviceCollectionService.Command;
using DeviceCollectionService.Common;
using DeviceCollectionService.Entity;
using DeviceCollectionService.IBLL;

namespace DeviceCollectionService
{
    public class WorkerService : BackgroundService
    {
        private readonly ILogger<WorkerService> _logger;
        private readonly IWorkerBLL _workerBLL;
        private readonly ILoginBLL _loginBLL;
        private readonly ILineBLL _lineBLL;
        private readonly LocalTool _localSetting;
        private readonly GlobalValue _globalValue;
        private readonly LocalTool _localTool;

        public WorkerService(ILogger<WorkerService> logger, LocalTool localTool, ILoginBLL loginBLL, ILineBLL lineBLL, IWorkerBLL workerBLL, LocalTool localSetting, GlobalValue globalValue)
        {
            _logger = logger;
            _workerBLL = workerBLL;
            _loginBLL = loginBLL;
            _localSetting = localSetting;
            _globalValue = globalValue;
            _localTool = localTool;
            _lineBLL = lineBLL;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            //_logger.LogInformation("Worker starting at: {time}", DateTimeOffset.Now);
            _localTool.InsertLogger(_logger, "StartAsync", "启动服务");
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Appsettings appsettings = _localSetting.GetLocalSetting();
            _globalValue.serverBaseUrl = appsettings.UserInfo.serverBaseUrl;

            //登录
            PubEntityResponseNotT? pubEntityResponseNotT = await _loginBLL.LoginByUserNameAndPwd(appsettings.UserInfo.username, appsettings.UserInfo.password);
            _globalValue.PubEntityResponseNotT = pubEntityResponseNotT;

            //查询线路
            int count = await _lineBLL.GetEnableLineTotal();
            Parallel.For(0, count, i =>
            {
                Task.Run(() => RunTask(i, stoppingToken));
            });
        }

        private async void RunTask(int i, CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _localTool.InsertLogger(_logger, $"StartAsync{i}", "执行逻辑");
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _localTool.InsertLogger(_logger, "StopAsync", "停止服务");
            await base.StopAsync(cancellationToken);
        }
    }
}