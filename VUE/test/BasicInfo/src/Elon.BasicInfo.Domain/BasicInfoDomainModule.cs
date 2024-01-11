using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Elon.BasicInfo;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(BasicInfoDomainSharedModule)
)]
public class BasicInfoDomainModule : AbpModule
{

}
