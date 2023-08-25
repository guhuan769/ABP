using DeviceCollectionService.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.DAL
{
    public class LineDAL : ILineDAL
    {
        private readonly IWebDataAccess _webDataAccess;

        public LineDAL(IWebDataAccess webDataAccess)
        {
            _webDataAccess = webDataAccess;
        }

        public async Task<string> GetEnableLineTotal()
        {
            return await _webDataAccess.GetDatas("api/LineGetEnableLineTotal");
        }

        public async Task<string> InsertPart(string data)
        {
            StringContent content = new StringContent(data);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await _webDataAccess.PostDatas($"api/ProductionInsertPubProductionparts", content);
        }
    }
}
