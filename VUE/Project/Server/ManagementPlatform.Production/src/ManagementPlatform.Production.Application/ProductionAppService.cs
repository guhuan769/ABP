using ManagementPlatform.Production.Localization;
using Volo.Abp.Application.Services;

namespace ManagementPlatform.Production;

public abstract class ProductionAppService : ApplicationService
{
    protected ProductionAppService()
    {
        LocalizationResource = typeof(ProductionResource);
        ObjectMapperContext = typeof(ProductionApplicationModule);
    }
}
