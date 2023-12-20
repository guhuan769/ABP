using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Elon.ConfiguratioinCenter.Web;

[Dependency(ReplaceServices = true)]
public class ConfiguratioinCenterBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ConfiguratioinCenter";
}
