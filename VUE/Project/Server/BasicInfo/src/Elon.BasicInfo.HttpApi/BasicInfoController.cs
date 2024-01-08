using Elon.BasicInfo.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Elon.BasicInfo;

public abstract class BasicInfoController : AbpControllerBase
{
    protected BasicInfoController()
    {
        LocalizationResource = typeof(BasicInfoResource);
    }
}
