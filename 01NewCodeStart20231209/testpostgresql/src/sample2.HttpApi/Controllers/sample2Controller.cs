using sample2.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace sample2.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class sample2Controller : AbpControllerBase
{
    protected sample2Controller()
    {
        LocalizationResource = typeof(sample2Resource);
    }
}
