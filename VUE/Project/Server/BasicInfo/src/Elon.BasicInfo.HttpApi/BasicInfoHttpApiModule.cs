using Localization.Resources.AbpUi;
using Elon.BasicInfo.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Elon.BasicInfo;

[DependsOn(
    typeof(BasicInfoApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class BasicInfoHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(BasicInfoHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<BasicInfoResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
