using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace MicroClassroom.Enterprise;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(EnterpriseDomainSharedModule)
)]
public class EnterpriseDomainModule : AbpModule
{

}
