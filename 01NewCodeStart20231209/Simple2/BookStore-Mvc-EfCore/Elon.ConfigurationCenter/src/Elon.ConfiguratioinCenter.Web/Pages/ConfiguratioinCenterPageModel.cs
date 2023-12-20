using Elon.ConfiguratioinCenter.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Elon.ConfiguratioinCenter.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ConfiguratioinCenterPageModel : AbpPageModel
{
    protected ConfiguratioinCenterPageModel()
    {
        LocalizationResourceType = typeof(ConfiguratioinCenterResource);
    }
}
