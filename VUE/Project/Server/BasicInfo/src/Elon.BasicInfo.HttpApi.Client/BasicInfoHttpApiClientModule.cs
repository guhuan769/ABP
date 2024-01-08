using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Elon.BasicInfo;

[DependsOn(
    typeof(BasicInfoApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class BasicInfoHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(BasicInfoApplicationContractsModule).Assembly,
            BasicInfoRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<BasicInfoHttpApiClientModule>();
        });

    }
}
