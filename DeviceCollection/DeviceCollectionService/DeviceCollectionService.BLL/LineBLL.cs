using DeviceCollectionService.Common;
using DeviceCollectionService.Entity;
using DeviceCollectionService.IBLL;
using DeviceCollectionService.IDAL;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Reflection;

namespace DeviceCollectionService.BLL
{
    public class LineBLL : ILineBLL
    {
        string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
        private readonly ILineDAL _lineDAL;
        private readonly ILogger _logger;
        private readonly LocalTool _localTool;
        public LineBLL(ILoggerFactory loggerFactory, ILineDAL lineDAL, LocalTool localTool)
        {
            _lineDAL = lineDAL;
            _localTool = localTool;
            _logger = loggerFactory.CreateLogger<LoginBLL>();
        }
        public async Task<PubEntityResponse<PubProductionlineinfoResponse>> GetEnableLineTotal()
        {
            string result = await _lineDAL.GetEnableLineTotal();
            PubEntityResponse<PubProductionlineinfoResponse>? pubEntityResponseNotT = JsonConvert.DeserializeObject<PubEntityResponse<PubProductionlineinfoResponse>>(result);
            if (pubEntityResponseNotT != null)
            {
                if (pubEntityResponseNotT.Code == 0)
                {
                    return pubEntityResponseNotT;
                }
                else
                {
                    string methodName = MethodBase.GetCurrentMethod().DeclaringType.Name;
                    _localTool.InsertLogger(_logger, $"{className}->{methodName}", pubEntityResponseNotT.Msg);
                }
            }
            return pubEntityResponseNotT;
        }
    }
}
