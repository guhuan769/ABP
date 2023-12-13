using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;
using Elon.DashboardCenter.Application.Contracts;
using Elon.DashboardCenter.Application;

namespace Elon.DashboardCenter.HttpApi
{
    [DependsOn(typeof(DashboardCenterApplicationContractsModule))]
    public class DashboardCenterHttpApiModule:AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(DashboardCenterApplicationModule).Assembly);
            });// Auto API暴露
        }
    }
}
