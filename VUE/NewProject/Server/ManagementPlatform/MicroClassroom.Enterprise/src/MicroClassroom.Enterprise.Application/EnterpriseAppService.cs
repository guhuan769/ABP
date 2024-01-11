using MicroClassroom.Enterprise.Localization;
using Volo.Abp.Application.Services;

namespace MicroClassroom.Enterprise;

public abstract class EnterpriseAppService : ApplicationService
{
    protected EnterpriseAppService()
    {
        LocalizationResource = typeof(EnterpriseResource);
        ObjectMapperContext = typeof(EnterpriseApplicationModule);
    }
}
