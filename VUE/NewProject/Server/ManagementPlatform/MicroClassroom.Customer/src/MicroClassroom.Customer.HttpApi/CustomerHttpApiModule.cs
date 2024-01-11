using Localization.Resources.AbpUi;
using MicroClassroom.Customer.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace MicroClassroom.Customer;

[DependsOn(
    typeof(CustomerApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class CustomerHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CustomerHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<CustomerResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
