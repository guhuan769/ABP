using Elon.DashboardCenter.Application.Contracts;
using Elon.DashboardCenter.Application.Contracts.LogDashboards;
using Elon.DashboardCenter.Application.LogDashboards;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Elon.DashboardCenter.Application
{
    [DependsOn(typeof(DashboardCenterApplicationContractsModule))] // 依赖某个模块
    public class DashboardCenterApplicationModule: AbpModule
    {
        /// <summary>
        /// DI 配置
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //常规玩法 在ABP中是标记接口
            //context.Services.AddSingleton<ILogDashboardsAppService, LogDashboardsAppService>();

            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"This is {this.GetType().Name} {MethodInfo.GetCurrentMethod()!.Name}");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Pre 是执行前
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"This is {this.GetType().Name} {MethodInfo.GetCurrentMethod()!.Name}");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// post 是执行后
        /// </summary>
        /// <param name="context"></param>
        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"This is {this.GetType().Name} {MethodInfo.GetCurrentMethod()!.Name}");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// 程序得初始化 -- 等同于 Startup里面得Configure方法 
        /// </summary>
        /// <param name="context"></param>
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            //去找IOC容器获取实例 --- 如果能成功，表示已经注册了，表示发生在 ConfigureServices 
            var service = context.ServiceProvider.GetService<ILogDashboardsAppService>();
            service!.GetLogDashboardsAsync();

            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"This is {this.GetType().Name} {MethodInfo.GetCurrentMethod()!.Name}");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"This is {this.GetType().Name} {MethodInfo.GetCurrentMethod()!.Name}");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"This is {this.GetType().Name} {MethodInfo.GetCurrentMethod()!.Name}");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// 当程序正常退出时 
        /// </summary>
        /// <param name="context"></param>
        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"This is {this.GetType().Name} {MethodInfo.GetCurrentMethod()!.Name}");
            Console.BackgroundColor = ConsoleColor.Black;
        }

    }
}
