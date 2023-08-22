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
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<IWorkerBLL, WorkerBLL>();
                    services.AddTransient<ILoginDAL, LoginDAL>();
                    services.AddTransient<ILoginBLL, LoginBLL>();
                    services.AddTransient<IWebDataAccess, WebDataAccess>();
                    services.AddSingleton<LocalTool>();
                    services.AddSingleton<GlobalValue>();
                    services.AddHostedService<WorkerService>();
                });
    }
}
