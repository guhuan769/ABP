using Volo.Abp.Modularity;

namespace MicroClassroom.Enterprise;

[DependsOn(
    typeof(EnterpriseApplicationModule),
    typeof(EnterpriseDomainTestModule)
    )]
public class EnterpriseApplicationTestModule : AbpModule
{

}
