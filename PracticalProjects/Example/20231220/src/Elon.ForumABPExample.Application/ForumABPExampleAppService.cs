using System;
using System.Collections.Generic;
using System.Text;
using Elon.ForumABPExample.Localization;
using Volo.Abp.Application.Services;

namespace Elon.ForumABPExample;

/* Inherit your application services from this class.
 */
public abstract class ForumABPExampleAppService : ApplicationService
{
    protected ForumABPExampleAppService()
    {
        LocalizationResource = typeof(ForumABPExampleResource);
    }
}
