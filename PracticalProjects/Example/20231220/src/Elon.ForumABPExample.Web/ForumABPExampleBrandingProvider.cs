using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Elon.ForumABPExample.Web;

[Dependency(ReplaceServices = true)]
public class ForumABPExampleBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ForumABPExample";
}
