using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;
using Elon.DashboardCenter.Application.Contracts;
using Elon.DashboardCenter.Application;
using Elon.DashboardCenter.Application.ErrorDashboards;

namespace Elon.DashboardCenter.HttpApi
{
    [DependsOn(typeof(DashboardCenterApplicationContractsModule))]
    public class DashboardCenterHttpApiModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof
                    (DashboardCenterApplicationModule).Assembly,
                    o =>
                    {
                        o.RootPath = "dashboard2";
                        o.TypePredicate = s => !s.Name.Equals(nameof(ErrorDashboardsAppService));
                    });
            });// Auto API暴露
        }
    }
}
