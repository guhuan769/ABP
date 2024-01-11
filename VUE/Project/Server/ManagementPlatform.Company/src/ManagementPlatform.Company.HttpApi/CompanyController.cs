using ManagementPlatform.Company.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ManagementPlatform.Company;

public abstract class CompanyController : AbpControllerBase
{
    protected CompanyController()
    {
        LocalizationResource = typeof(CompanyResource);
    }
}
