using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace MicroClassroom.Customer;

[DependsOn(
    typeof(CustomerDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class CustomerApplicationContractsModule : AbpModule
{

}
