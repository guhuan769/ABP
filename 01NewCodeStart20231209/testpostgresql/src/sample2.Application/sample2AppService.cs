using System;
using System.Collections.Generic;
using System.Text;
using sample2.Localization;
using Volo.Abp.Application.Services;

namespace sample2;

/* Inherit your application services from this class.
 */
public abstract class sample2AppService : ApplicationService
{
    protected sample2AppService()
    {
        LocalizationResource = typeof(sample2Resource);
    }
}
