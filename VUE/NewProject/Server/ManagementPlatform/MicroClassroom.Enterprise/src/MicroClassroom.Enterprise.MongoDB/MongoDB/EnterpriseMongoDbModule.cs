using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace MicroClassroom.Enterprise.MongoDB;

[DependsOn(
    typeof(EnterpriseDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class EnterpriseMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<EnterpriseMongoDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
        });
    }
}
