using Elon.ConfiguratioinCenter.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Elon.ConfiguratioinCenter.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ConfiguratioinCenterController : AbpControllerBase
{
    protected ConfiguratioinCenterController()
    {
        LocalizationResource = typeof(ConfiguratioinCenterResource);
    }
}
