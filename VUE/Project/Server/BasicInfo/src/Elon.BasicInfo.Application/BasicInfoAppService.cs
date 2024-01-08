using Elon.BasicInfo.Localization;
using Volo.Abp.Application.Services;

namespace Elon.BasicInfo;

public abstract class BasicInfoAppService : ApplicationService
{
    protected BasicInfoAppService()
    {
        LocalizationResource = typeof(BasicInfoResource);
        ObjectMapperContext = typeof(BasicInfoApplicationModule);
    }
}
