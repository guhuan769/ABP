using Elon.ForumABPExample.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Elon.ForumABPExample.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ForumABPExampleController : AbpControllerBase
{
    protected ForumABPExampleController()
    {
        LocalizationResource = typeof(ForumABPExampleResource);
    }
}
