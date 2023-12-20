using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Elon.ConfiguratioinCenter.Data;

/* This is used if database provider does't define
 * IConfiguratioinCenterDbSchemaMigrator implementation.
 */
public class NullConfiguratioinCenterDbSchemaMigrator : IConfiguratioinCenterDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
