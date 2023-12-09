using sample2.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace sample2.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class sample2PageModel : AbpPageModel
{
    protected sample2PageModel()
    {
        LocalizationResourceType = typeof(sample2Resource);
    }
}
