using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace ManagementPlatform.Company;

[DependsOn(
    typeof(CompanyApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class CompanyHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(CompanyApplicationContractsModule).Assembly,
            CompanyRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CompanyHttpApiClientModule>();
        });

    }
}
