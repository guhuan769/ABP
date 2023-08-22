using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.IBLL
{
    public interface ILineBLL
    {
        Task<int> GetEnableLineTotal();
    }
}
