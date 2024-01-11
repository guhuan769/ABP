using ManagementPlatform.Identity.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ManagementPlatform.Identity.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class IdentityController : AbpControllerBase
{
    protected IdentityController()
    {
        LocalizationResource = typeof(IdentityResource);
    }
}
