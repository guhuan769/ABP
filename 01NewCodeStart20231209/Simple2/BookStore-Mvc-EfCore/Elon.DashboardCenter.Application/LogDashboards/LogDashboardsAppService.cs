using Elon.DashboardCenter.Application.Contracts.LogDashboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Elon.DashboardCenter.Application.LogDashboards
{
    public class LogDashboardsAppService : ILogDashboardsAppService
        ,ITransientDependency//标记接口 自动注入
       ,IRemoteService //暴露出来
    {
        /// <summary>
        /// 获取信息 --- 打印个日志
        /// </summary>
        public LogDto GetLogDashboardsAsync()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"This is {this.GetType().Name} {MethodInfo.GetCurrentMethod()!.Name}");
            Console.BackgroundColor = ConsoleColor.Black;
            return new LogDto() { 
                Id = 1,
                Name = "Elon",
                Description = $"This is {this.GetType().Name} {MethodInfo.GetCurrentMethod()!.Name}"
            };
        }
    }
}
