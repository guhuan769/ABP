using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elon.DashboardCenter.Application.Contracts.LogDashboards
{
    public interface ILogDashboardsAppService
    {
        public LogDto GetLogDashboardsAsync();
    }
}
