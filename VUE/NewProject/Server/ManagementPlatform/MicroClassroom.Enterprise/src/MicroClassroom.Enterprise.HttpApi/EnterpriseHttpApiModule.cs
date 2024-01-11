using Localization.Resources.AbpUi;
using MicroClassroom.Enterprise.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace MicroClassroom.Enterprise;

[DependsOn(
    typeof(EnterpriseApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class EnterpriseHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(EnterpriseHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<EnterpriseResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
