using DeviceCollectionService.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.DAL
{
    public class LineDAL:ILineDAL
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
    }
}
