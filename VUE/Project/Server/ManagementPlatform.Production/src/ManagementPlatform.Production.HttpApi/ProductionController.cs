using ManagementPlatform.Production.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ManagementPlatform.Production;

public abstract class ProductionController : AbpControllerBase
{
    protected ProductionController()
    {
        LocalizationResource = typeof(ProductionResource);
    }
}
