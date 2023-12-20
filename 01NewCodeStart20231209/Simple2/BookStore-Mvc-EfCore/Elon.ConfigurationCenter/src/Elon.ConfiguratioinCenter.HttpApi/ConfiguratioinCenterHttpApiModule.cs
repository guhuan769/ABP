using Localization.Resources.AbpUi;
using Elon.ConfiguratioinCenter.Localization;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.AspNetCore.Mvc;

namespace Elon.ConfiguratioinCenter;

[DependsOn(
    typeof(ConfiguratioinCenterApplicationContractsModule),
    typeof(ConfiguratioinCenterApplicationModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpTenantManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule)
    )]
public class ConfiguratioinCenterHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<ConfiguratioinCenterResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });

        Configure<AbpAspNetCoreMvcOptions>(options => {
            options.ConventionalControllers.Create(typeof(ConfiguratioinCenterApplicationModule).Assembly);
        });
    }
}
