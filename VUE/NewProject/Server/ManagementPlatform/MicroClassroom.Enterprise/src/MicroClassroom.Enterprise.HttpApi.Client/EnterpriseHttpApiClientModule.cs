using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace MicroClassroom.Enterprise;

[DependsOn(
    typeof(EnterpriseApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class EnterpriseHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(EnterpriseApplicationContractsModule).Assembly,
            EnterpriseRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EnterpriseHttpApiClientModule>();
        });
    }
}
