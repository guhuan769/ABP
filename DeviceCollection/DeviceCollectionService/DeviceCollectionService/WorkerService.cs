using DeviceCollectionService.BLL;
using DeviceCollectionService.Command;
using DeviceCollectionService.Common;
using DeviceCollectionService.Entity;
using DeviceCollectionService.IBLL;
using S7.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DeviceCollectionService
{
    public class WorkerService : BackgroundService
    {
        private readonly ILogger<WorkerService> _logger;
        private readonly IWorkerBLL _workerBLL;
        private readonly ILoginBLL _loginBLL;
        private readonly ILineBLL _lineBLL;
        private readonly IDeviceBLL _deviceBLL;
        private readonly LocalTool _localSetting;
        private readonly GlobalValue _globalValue;
        private readonly LocalTool _localTool;

        public WorkerService(ILogger<WorkerService> logger, LocalTool localTool, IDeviceBLL deviceBLL, ILoginBLL loginBLL, ILineBLL lineBLL, IWorkerBLL workerBLL, LocalTool localSetting, GlobalValue globalValue)
        {
            _logger = logger;
            _workerBLL = workerBLL;
            _loginBLL = loginBLL;
            _localSetting = localSetting;
            _globalValue = globalValue;
            _localTool = localTool;
            _lineBLL = lineBLL;
            _deviceBLL = deviceBLL;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            //_logger.LogInformation("Worker starting at: {time}", DateTimeOffset.Now);
            _localTool.InsertLogger(_logger, "StartAsync", "Start Server");
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Appsettings appsettings = _localSetting.GetLocalSetting();
            _globalValue.serverBaseUrl = appsettings.UserInfo.serverBaseUrl;

            while (true)
            {
                bool flag = Login(appsettings).GetAwaiter().GetResult();
                if (!flag)
                {
                    await Task.Delay(5000);
                    continue;
                }
                break;
            }

            //��ѯ��·
            PubEntityResponse<PubProductionlineinfoResponse> pubEntityResponse = await _lineBLL.GetEnableLineTotal();
            Parallel.ForEach(pubEntityResponse.Data, entity =>
            {
                Task.Run(() => RunTask(entity, stoppingToken));
            });

            //Parallel.For(0, count, i =>
            //{
            //    Task.Run(() => RunTask(i, stoppingToken));
            //});
        }

        private async Task<bool> Login(Appsettings appsettings)
        {
            try
            {
                //��¼
                PubEntityResponseNotT? result = await _loginBLL.LoginByUserNameAndPwd(appsettings.UserInfo.username, appsettings.UserInfo.password);
                if (result.Code == 0)
                {
                    _localTool.InsertLogger(_logger, "ExecuteAsync", "login successful");
                    _globalValue.PubEntityResponseNotT = result;
                    return true;
                }
                else
                {
                    _localTool.InsertLogger(_logger, "ExecuteAsync", $"Please try again after 5 seconds of login failure,ERROR:{result.Msg}");

                    return false;
                }
            }
            catch (Exception ex)
            {
                _localTool.InsertLogger(_logger, "ExecuteAsync", $"Please check whenther the server is started {ex.Message}");
                return false;
            }
        }

        private async void RunTask(PubProductionlineinfoResponse pubProductionlineinfoResponse, CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //������·��CODE ��ѯ�豸
                    List<DeviceOutResponse> devices = await _deviceBLL.GetDevice(pubProductionlineinfoResponse.code);
                    foreach (DeviceOutResponse device in devices)
                    {
                        PlcEntity plcEntity = new PlcEntity();
                        plcEntity.PlcIp = device.Ip;
                        bool isConnect = await _localTool.ConnectPlc(_logger, plcEntity);
                        if (!isConnect)
                        {
                            //PLC�Ͽ���־

                        }
                        string[] strArr = device.Ip.Split('&');
                        if (isConnect)
                        {
                            try
                            {
                                if (strArr[2].ToString().Equals("!bool"))
                                {
                                    bool statu = (bool)await plcEntity.S7Plc.ReadAsync(strArr[1]);
                                    long runtStatus = 0;
                                    if (statu)
                                    {
                                        runtStatus = 1;
                                    }
                                    else
                                    {
                                        runtStatus = 4;
                                    }
                                    bool deviceBool = await _deviceBLL.DeviceUpdateStatus(device.code, runtStatus);
                                }
                                else if (strArr[2].ToString().Equals("bool"))
                                {
                                    bool statu = (bool)await plcEntity.S7Plc.ReadAsync(strArr[1]);
                                    long runtStatus = 0;
                                    if (statu)
                                    {
                                        runtStatus = 4;
                                    }
                                    else
                                    {
                                        runtStatus = 1;
                                    }
                                    bool deviceBool = await _deviceBLL.DeviceUpdateStatus(device.code, runtStatus);
                                }
                                else if (strArr[2].ToString().Equals("double"))
                                {
                                    //��ȡ��·�ܲ���
                                    var total = await plcEntity.S7Plc.ReadAsync(strArr[1]);
                                }
                                else if (strArr[2].ToString().Equals("int"))
                                {
                                    //var aa = await plcEntity.S7Plc.ReadAsync(strArr[1]);
                                    int total = (int)((UInt16)await plcEntity.S7Plc.ReadAsync(strArr[1]));
                                }

                                //count = (int)((uint)await plcEntity.S7Plc.ReadAsync(strArr[1])).ConvertToFloat();
                                //var value = ((uint)).ConvertToFloat();
                            }
                            catch (Exception ex)
                            {
                                _localTool.InsertLogger(_logger, "StopAsync", $"��ȡ{device.tName}����ʧ�ܣ�{ex.Message},��������Plc");
                                plcEntity.S7Plc.Close();
                            }
                        }
                    }
                    //_localTool.InsertLogger(_logger, "RunTask", $" start ");
                    //_localTool.InsertLogger(_logger, $"StartAsync{pubProductionlineinfoResponse.code}", "ִ���߼�");
                    await Task.Delay(1000, stoppingToken);
                }
                catch (Exception ex)
                {

                    _localTool.InsertLogger(_logger, "RunTask", $"Service disconnected, please try to reconnect {ex.Message}");
                    Appsettings appsettings = _localSetting.GetLocalSetting();
                    _globalValue.serverBaseUrl = appsettings.UserInfo.serverBaseUrl;
                    while (true)
                    {
                        bool flag = Login(appsettings).GetAwaiter().GetResult();
                        if (!flag)
                        {
                            await Task.Delay(5000);
                            continue;
                        }
                        break;
                    }
                }
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _localTool.InsertLogger(_logger, "StopAsync", "Stop Server");
            await base.StopAsync(cancellationToken);
        }
    }
}