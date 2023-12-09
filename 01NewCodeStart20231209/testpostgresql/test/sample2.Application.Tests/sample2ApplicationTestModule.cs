using Volo.Abp.Modularity;

namespace sample2;

[DependsOn(
    typeof(sample2ApplicationModule),
    typeof(sample2DomainTestModule)
    )]
public class sample2ApplicationTestModule : AbpModule
{

}
