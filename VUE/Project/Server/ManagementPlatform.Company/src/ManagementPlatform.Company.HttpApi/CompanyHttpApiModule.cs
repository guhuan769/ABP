using Localization.Resources.AbpUi;
using ManagementPlatform.Company.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace ManagementPlatform.Company;

[DependsOn(
    typeof(CompanyApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class CompanyHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CompanyHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<CompanyResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
