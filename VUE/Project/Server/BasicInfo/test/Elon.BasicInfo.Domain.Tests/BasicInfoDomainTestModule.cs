using Volo.Abp.Modularity;

namespace Elon.BasicInfo;

[DependsOn(
    typeof(BasicInfoDomainModule),
    typeof(BasicInfoTestBaseModule)
)]
public class BasicInfoDomainTestModule : AbpModule
{

}
