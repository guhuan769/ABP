using Elon.Production.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Elon.Production;

public abstract class ProductionController : AbpControllerBase
{
    protected ProductionController()
    {
        LocalizationResource = typeof(ProductionResource);
    }
}
