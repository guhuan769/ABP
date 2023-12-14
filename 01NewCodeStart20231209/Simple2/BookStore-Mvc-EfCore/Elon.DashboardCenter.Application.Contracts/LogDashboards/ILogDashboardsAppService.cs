using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elon.DashboardCenter.Application.Contracts.LogDashboards
{
    public interface ILogDashboardsAppService
    {
        /// <summary>
        /// 约定俗成的
        /// </summary>
        /// <returns></returns>
        public LogDto GetLogDashboardsAsync();

        //public LogDto GetListLogDashboardsAsync();
        //public LogDto GetAllLogDashboardsAsync();

        //public Task CreateLogDashboardsAsync(LogDto logDto);
        //public Task UpdateLogDashboardsAsync(LogDto logDto);
        //public Task AddLogDashboardsAsync(LogDto logDto);
        //public Task PostLogDashboardsAsync(LogDto logDto);
        ///// <summary>
        ///// 默认Post
        ///// </summary>
        ///// <param name="logDto"></param>
        ///// <returns></returns>
        //public Task CustomLogDashboardsAsync(LogDto logDto);

        //[HttpGet]
        //public Task CustomGetLogDashboardsAsync(LogDto logDto);
    }
}
