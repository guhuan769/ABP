using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace ManagementPlatform.Company;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class CompanyInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CompanyInstallerModule>();
        });
    }
}
