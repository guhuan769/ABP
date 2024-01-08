using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Elon.BasicInfo;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class BasicInfoInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<BasicInfoInstallerModule>();
        });
    }
}
