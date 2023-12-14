using Elon.DashboardCenter.Application.Contracts.ErrorDashboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp;

namespace Elon.DashboardCenter.Application.ErrorDashboards
{
    [RemoteService(IsEnabled = false)]
    public class ErrorDashboardsAppService : IErrorDashboardsAppService, ITransientDependency//标记接口 自动注入
    {
        public async Task GetErrorDashboardsAsync()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"This is {this.GetType().Name} {MethodInfo.GetCurrentMethod()!.Name}");
            Console.BackgroundColor = ConsoleColor.Black;
            
            await Task.CompletedTask;
        }
    }
}
