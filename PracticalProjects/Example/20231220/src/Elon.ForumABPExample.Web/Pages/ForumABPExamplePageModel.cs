using Elon.ForumABPExample.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Elon.ForumABPExample.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ForumABPExamplePageModel : AbpPageModel
{
    protected ForumABPExamplePageModel()
    {
        LocalizationResourceType = typeof(ForumABPExampleResource);
    }
}
