using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.IDAL
{
    public interface ILoginDAL
    {
        Task<string> LoginByUserNameAndPwd(string data);
    }
}
