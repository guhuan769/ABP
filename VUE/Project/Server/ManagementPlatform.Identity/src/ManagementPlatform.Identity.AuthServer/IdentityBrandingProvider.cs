using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace ManagementPlatform.Identity;

[Dependency(ReplaceServices = true)]
public class IdentityBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Identity";
}
