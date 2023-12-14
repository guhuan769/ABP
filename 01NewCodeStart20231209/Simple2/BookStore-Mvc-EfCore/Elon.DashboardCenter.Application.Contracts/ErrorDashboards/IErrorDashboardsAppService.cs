using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Elon.DashboardCenter.Application.Contracts.ErrorDashboards
{
    
    public interface IErrorDashboardsAppService: IRemoteService //标记成自动API
    {
        public Task GetErrorDashboardsAsync();
    }
}
