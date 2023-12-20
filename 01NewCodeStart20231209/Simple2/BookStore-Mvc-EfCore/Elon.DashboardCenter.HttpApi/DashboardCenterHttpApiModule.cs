using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;
using Elon.DashboardCenter.Application.Contracts;
using Elon.DashboardCenter.Application;
using Elon.DashboardCenter.Application.ErrorDashboards;
using Elon.DashboardCenter.Application.LogDashboards;
using Microsoft.Extensions.DependencyInjection;

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

            context.Services.Configure<LogDashboardsOptions>(option =>
            {
                option.DashboardDescription = $"this is {nameof(DashboardCenterHttpApiModule)}" +
                $"ConfigureServices DashboardDescription " +
                $"";
            });

            context.Services.ConfigureAll<LogDashboardsOptions>(option =>
            {
                option.DashboardName = $"this is {nameof(DashboardCenterHttpApiModule)}" +
                $"ConfigureServices DashboardDescription " +
                $"";
            });

            context.Services.PostConfigure<LogDashboardsOptions>(option =>
            {
                option.DashboardName = $"this is {nameof(DashboardCenterHttpApiModule)}" +
                $"ConfigureServices DashboardDescription " +
                $"";
            });

            context.Services.PostConfigureAll<LogDashboardsOptions>(option =>
            {
                option.DashboardName = $"this is {nameof(DashboardCenterHttpApiModule)}" +
                $"ConfigureServices DashboardDescription " +
                $"";
            });

            //Configure<LogDashboardsOptions>(options => {
            //    options.
            //});
        }
    }
}
