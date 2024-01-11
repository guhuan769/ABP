using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Identity;

namespace MicroClassroom.Enterprise;

[DependsOn(
    typeof(EnterpriseDomainModule),
    typeof(AbpIdentityDomainModule),
    typeof(EnterpriseApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class EnterpriseApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<EnterpriseApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<EnterpriseApplicationModule>(validate: true);
        });
    }
}
