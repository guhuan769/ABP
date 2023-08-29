using DeviceCollectionService.Common;
using DeviceCollectionService.Entity;
using DeviceCollectionService.IBLL;
using DeviceCollectionService.IDAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Reflection;

namespace DeviceCollectionService.BLL
{
    public class LoginBLL : ILoginBLL
    {
        string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
        private readonly ILoginDAL _loginDAL;
        private readonly ILogger _logger;
        private readonly LocalTool _localTool;
        public LoginBLL(ILoggerFactory loggerFactory, ILoginDAL loginDA, LocalTool localTool)
        {
            _loginDAL = loginDA;
            _localTool = localTool;
            _logger = loggerFactory.CreateLogger<LoginBLL>();
        }

        public async Task<PubEntityResponseNotT?> LoginByUserNameAndPwd(string username, string password)
        {
            string userinfo = "{\"userName\":\"" + username + "\",\"password\":\"" + password + "\"}";
            string result = await _loginDAL.LoginByUserNameAndPwd(userinfo);
            if (string.IsNullOrEmpty(result))
            {
                return null;
            }
            PubEntityResponseNotT? pubEntityResponseNotT = JsonConvert.DeserializeObject<PubEntityResponseNotT>(result);
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
