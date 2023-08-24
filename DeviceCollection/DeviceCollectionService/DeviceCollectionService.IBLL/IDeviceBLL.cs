using DeviceCollectionService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.IBLL
{
    public interface IDeviceBLL
    {
        Task<bool> DeviceUpdateStatus(Guid code,long runtStatus);
        Task<List<DeviceOutResponse>> GetDevice(long code);
    }
}
