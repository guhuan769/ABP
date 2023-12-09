using sample2.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace sample2;

[DependsOn(
    typeof(sample2EntityFrameworkCoreTestModule)
    )]
public class sample2DomainTestModule : AbpModule
{

}
