using Volo.Abp.Modularity;

namespace Elon.BasicInfo;

[DependsOn(
    typeof(BasicInfoApplicationModule),
    typeof(BasicInfoDomainTestModule)
    )]
public class BasicInfoApplicationTestModule : AbpModule
{

}
