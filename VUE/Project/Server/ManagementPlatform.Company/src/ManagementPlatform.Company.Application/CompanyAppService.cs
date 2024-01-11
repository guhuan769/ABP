using ManagementPlatform.Company.Localization;
using Volo.Abp.Application.Services;

namespace ManagementPlatform.Company;

public abstract class CompanyAppService : ApplicationService
{
    protected CompanyAppService()
    {
        LocalizationResource = typeof(CompanyResource);
        ObjectMapperContext = typeof(CompanyApplicationModule);
    }
}
