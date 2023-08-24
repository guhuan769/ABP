using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.IDAL
{
    public interface IDeviceDAL
    {
        Task<string> DeviceUpdateStatus(Guid code, long runtStatus);
        Task<string> GetDeviceList(long code);
    }
}
