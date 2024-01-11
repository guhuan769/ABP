using Elon.Production.Localization;
using Volo.Abp.Application.Services;

namespace Elon.Production;

public abstract class ProductionAppService : ApplicationService
{
    protected ProductionAppService()
    {
        LocalizationResource = typeof(ProductionResource);
        ObjectMapperContext = typeof(ProductionApplicationModule);
    }
}
