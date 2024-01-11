using MicroClassroom.Customer.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MicroClassroom.Customer;

public abstract class CustomerController : AbpControllerBase
{
    protected CustomerController()
    {
        LocalizationResource = typeof(CustomerResource);
    }
}
