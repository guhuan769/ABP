using System;
using System.Collections.Generic;
using System.Text;
using Elon.ConfiguratioinCenter.Localization;
using Volo.Abp.Application.Services;

namespace Elon.ConfiguratioinCenter;

/* Inherit your application services from this class.
 */
public abstract class ConfiguratioinCenterAppService : ApplicationService
{
    protected ConfiguratioinCenterAppService()
    {
        LocalizationResource = typeof(ConfiguratioinCenterResource);
    }
}
