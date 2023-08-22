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
        private readonly LocalTool _localSetting;
        private readonly GlobalValue _globalValue;

        public WorkerService(ILogger<WorkerService> logger, ILoginBLL loginBLL, IWorkerBLL workerBLL, LocalTool localSetting, GlobalValue globalValue)
        {
            _logger = logger;
            _workerBLL = workerBLL;
            _loginBLL = loginBLL;
            _localSetting = localSetting;
            _globalValue = globalValue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            Appsettings appsettings = _localSetting.GetLocalSetting();
            _globalValue.serverBaseUrl = appsettings.UserInfo.serverBaseUrl;

            //登录
            PubEntityResponseNotT? pubEntityResponseNotT = await _loginBLL.LoginByUserNameAndPwd(appsettings.UserInfo.username, appsettings.UserInfo.password);
            _globalValue.PubEntityResponseNotT = pubEntityResponseNotT;

            //查询数据库线路
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    string str = _workerBLL.Test();
            //    _logger.LogInformation("Worker running at: {time}" + $"{str}", DateTimeOffset.Now);
            //    //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}