using DeviceCollectionService.Common;
using DeviceCollectionService.Entity;
using DeviceCollectionService.IBLL;
using Microsoft.Extensions.Logging;

namespace DeviceCollectionService.BLL
{
    public class WorkerBLL : IWorkerBLL
    {
        private readonly IDeviceBLL _deviceBLL;
        private readonly ILineBLL _lineBLL;
        private readonly ILogger _logger;
        private readonly LocalTool _localTool;
        public WorkerBLL(ILoggerFactory loggerFactory, IDeviceBLL deviceBLL, ILineBLL lineBLL, LocalTool localTool)
        {
            _deviceBLL = deviceBLL;
            _lineBLL = lineBLL;
            _localTool = localTool;
            _logger = loggerFactory.CreateLogger<WorkerBLL>();
        }

        /// <summary>
        /// 修改PLC状态
        /// </summary>
        /// <param name="device"></param>
        /// <param name="plcEntity"></param>
        /// <param name="strArr"></param>
        /// <param name="isRun">检测设备是否运行</param>
        /// <returns></returns>
        public async Task<bool> PlcUpdateType(DeviceOutResponse device, PlcEntity plcEntity, string[] strArr, bool isRun)
        {
            try
            {
                if (strArr[2].ToString().Equals("!bool"))
                {
                    long runtStatus = 2;
                    // 测试是否能Ping 通 
                    //Console.WriteLine("开始");
                    //_localTool.InsertLogger(_logger, "PlcUpdateType->", $"IP地址是{plcEntity.PlcIp} 当前IP状态是 -》{_localTool.IpPing(plcEntity.PlcIp.Split('&')[0])}");
                    if (_localTool.IpPing(plcEntity.PlcIp.Split('&')[0]))
                    {
                        if (isRun)
                        {
                            bool statu = (bool)await plcEntity.S7Plc.ReadAsync(strArr[1]);
                            if (statu)
                            {
                                runtStatus = 1;
                            }
                            else
                            {
                                runtStatus = 4;
                            }
                        }
                    }
                    bool deviceBool = await _deviceBLL.DeviceUpdateStatus(device.code, runtStatus);
                    return deviceBool;
                }
                else if (strArr[2].ToString().Equals("bool"))
                {
                    long runtStatus = 2;
                    //Console.WriteLine($"开始{plcEntity.PlcIp.Split('&')[0]}");
                    //_localTool.InsertLogger(_logger, "PlcUpdateType->", $"IP地址是{plcEntity.PlcIp} 当前IP状态是 -》{_localTool.IpPing(plcEntity.PlcIp.Split('&')[0])}");
                    bool state = _localTool.IpPing(plcEntity.PlcIp.Split('&')[0]);
                    if (state)
                    {
                        if (isRun)
                        {
                            bool statu = (bool)await plcEntity.S7Plc.ReadAsync(strArr[1]);
                            if (statu)
                            {
                                runtStatus = 4;
                            }
                            else
                            {
                                runtStatus = 1;
                            }
                        }
                    }
                    bool deviceBool = await _deviceBLL.DeviceUpdateStatus(device.code, runtStatus);
                    return deviceBool;
                }
                else if (strArr[2].ToString().Equals("double"))
                {
                    long runtStatus = 2;
                    if (isRun)
                    {
                        //获取线路产量 本身应该今日产量 但是PLC工程师没有提供地址
                        long total = (UInt32)await plcEntity.S7Plc.ReadAsync(strArr[1]);
                        InsertPubProductionparts parts = new InsertPubProductionparts();
                        parts.fromPubProductionparts.production = Convert.ToInt32(total);
                        parts.fromPubProductionparts.DeviceCode = device.code;
                        parts.fromPubProductionparts.productionLineCode = device.lineCode;
                        if (total != 0)
                        {
                            bool partBool = await _lineBLL.InsertPart(parts);
                            //打印日志
                            //_localTool.InsertLogger(_logger, "PlcUpdateType->", "PLC取出数据为0");
                        }
                    }
                }
                else if (strArr[2].ToString().Equals("int"))
                {
                    long runtStatus = 2;
                    if (isRun)
                    {
                        //var aa = await plcEntity.S7Plc.ReadAsync(strArr[1]);
                        int total = (int)((UInt16)await plcEntity.S7Plc.ReadAsync(strArr[1]));
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
    }
}