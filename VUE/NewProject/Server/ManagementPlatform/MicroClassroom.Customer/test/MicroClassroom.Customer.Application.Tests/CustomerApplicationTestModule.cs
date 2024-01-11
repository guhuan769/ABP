using Volo.Abp.Modularity;

namespace MicroClassroom.Customer;

[DependsOn(
    typeof(CustomerApplicationModule),
    typeof(CustomerDomainTestModule)
    )]
public class CustomerApplicationTestModule : AbpModule
{

}
