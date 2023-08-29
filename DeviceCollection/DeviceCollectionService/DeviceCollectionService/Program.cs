using DeviceCollectionService;
using DeviceCollectionService.BLL;
using DeviceCollectionService.Command;
using DeviceCollectionService.Common;
using DeviceCollectionService.DAL;
using DeviceCollectionService.IBLL;
using DeviceCollectionService.IDAL;

namespace DemoWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService(options =>
            {
                options.ServiceName = "MES系统数据采集服务";
                
            }).ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<IWorkerBLL, WorkerBLL>();

                    services.AddTransient<ILoginDAL, LoginDAL>();
                    services.AddTransient<ILoginBLL, LoginBLL>();

                    services.AddTransient<ILineDAL, LineDAL>();
                    services.AddTransient<ILineBLL, LineBLL>();

                    services.AddTransient<IDeviceBLL, DeviceBLL>();
                    services.AddTransient<IDeviceDAL, DeviceDAL>();

                    services.AddTransient<IWebDataAccess, WebDataAccess>();
                    services.AddSingleton<LocalTool>();
                    services.AddSingleton<GlobalValue>();
                    services.AddHostedService<WorkerService>();
                });
    }
}
