using DeviceCollectionService.Common;
using DeviceCollectionService.Entity;
using DeviceCollectionService.IBLL;
using DeviceCollectionService.IDAL;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.BLL
{
    public class DeviceBLL : IDeviceBLL
    {
        string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
        private readonly IDeviceDAL _deviceDal;
        private readonly ILogger _logger;
        private readonly LocalTool _localTool;
        public DeviceBLL(ILoggerFactory loggerFactory, IDeviceDAL deviceDal, LocalTool localTool)
        {
            _deviceDal = deviceDal;
            _localTool = localTool;
            _logger = loggerFactory.CreateLogger<LoginBLL>();
        }
        public async Task<List<DeviceOutResponse>> GetDevice(long code)
        {
            string result = await _deviceDal.GetDeviceList(code);
            PubEntityResponse<DeviceOutResponse>? pubEntityResponseNotT = JsonConvert.DeserializeObject<PubEntityResponse<DeviceOutResponse>>(result);
            if (pubEntityResponseNotT != null)
            {
                if (pubEntityResponseNotT.Code == 0)
                {
                    return pubEntityResponseNotT.Data;
                }
                else
                {
                    string methodName = MethodBase.GetCurrentMethod().DeclaringType.Name;
                    _localTool.InsertLogger(_logger, $"{className}->{methodName}", pubEntityResponseNotT.Msg);
                }
            }
            return pubEntityResponseNotT.Data;
        }

        public async Task<bool> DeviceUpdateStatus(Guid code, long runtStatus)
        {
            string result = await _deviceDal.DeviceUpdateStatus(code, runtStatus);
            PubEntityResponse<bool>? re = JsonConvert.DeserializeObject<PubEntityResponse<bool>>(result);
            if (re != null)
            {
                if (re.Code == 0)
                {
                    return re.DataEntity;
                }
                else
                {
                    //日志 异常码 
                    string methodName = MethodBase.GetCurrentMethod().DeclaringType.Name;
                    _localTool.InsertLogger(_logger, $"{className}->{methodName}", re.Msg);
                }
            }
            return re.DataEntity;
        }
    }
}
