using DeviceCollectionService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.IBLL
{
    public interface ILoginBLL
    {
        Task<PubEntityResponseNotT?> LoginByUserNameAndPwd(string username, string password);
    }
}
