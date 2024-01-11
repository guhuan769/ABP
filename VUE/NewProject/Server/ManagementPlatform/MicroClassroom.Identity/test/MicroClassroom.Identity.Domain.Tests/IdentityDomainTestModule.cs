using MicroClassroom.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MicroClassroom.Identity;

[DependsOn(
    typeof(IdentityEntityFrameworkCoreTestModule)
    )]
public class IdentityDomainTestModule : AbpModule
{

}
