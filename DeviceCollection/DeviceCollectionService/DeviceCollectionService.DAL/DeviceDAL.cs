using DeviceCollectionService.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DeviceCollectionService.DAL
{
    public class DeviceDAL : IDeviceDAL
    {
        private readonly IWebDataAccess _webDataAccess;

        public DeviceDAL(IWebDataAccess webDataAccess)
        {
            _webDataAccess = webDataAccess;
        }

        public async Task<string> DeviceUpdateStatus(Guid code, long runtStatus)
        { 
            StringContent content = new StringContent(code.ToString());
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await _webDataAccess.PutDatas($"api/ProductionUpdateDeviceByRunStatus?code={code}&runtStatus={runtStatus}", content);
        }

        public async Task<string> GetDeviceList(long code)
        {
            StringContent content = new StringContent(code.ToString());
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await _webDataAccess.PutDatas($"api/ProductionGetDevice?code={code}", content);
        }
    }
}
