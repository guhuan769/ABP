using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace sample2.Web;

[Dependency(ReplaceServices = true)]
public class sample2BrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "sample2";
}
