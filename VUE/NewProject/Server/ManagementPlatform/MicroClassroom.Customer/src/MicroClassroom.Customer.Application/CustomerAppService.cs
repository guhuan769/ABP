using MicroClassroom.Customer.Localization;
using Volo.Abp.Application.Services;

namespace MicroClassroom.Customer;

public abstract class CustomerAppService : ApplicationService
{
    protected CustomerAppService()
    {
        LocalizationResource = typeof(CustomerResource);
        ObjectMapperContext = typeof(CustomerApplicationModule);
    }
}
