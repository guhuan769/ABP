using MicroClassroom.Customer.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MicroClassroom.Customer;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(CustomerEntityFrameworkCoreTestModule)
    )]
public class CustomerDomainTestModule : AbpModule
{

}
