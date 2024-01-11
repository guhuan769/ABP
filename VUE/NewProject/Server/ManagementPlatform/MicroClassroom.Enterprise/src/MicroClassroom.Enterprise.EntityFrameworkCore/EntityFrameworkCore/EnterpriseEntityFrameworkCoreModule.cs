using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MicroClassroom.Enterprise.EntityFrameworkCore;

[DependsOn(
    typeof(EnterpriseDomainModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class EnterpriseEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<EnterpriseDbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */
            options.Entity<Mechanism>(options => options.DefaultWithDetailsFunc = x => x.IncludeDetails());
            options.AddDefaultRepositories(includeAllEntities: true);
        });
    }
}
