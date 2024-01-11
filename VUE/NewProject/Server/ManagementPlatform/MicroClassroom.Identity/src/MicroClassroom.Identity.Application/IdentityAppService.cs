using System;
using System.Collections.Generic;
using System.Text;
using MicroClassroom.Identity.Localization;
using Volo.Abp.Application.Services;

namespace MicroClassroom.Identity;

/* Inherit your application services from this class.
 */
public abstract class IdentityAppService : ApplicationService
{
    protected IdentityAppService()
    {
        LocalizationResource = typeof(IdentityResource);
    }
}
