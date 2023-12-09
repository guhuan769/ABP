using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace sample2.Data;

/* This is used if database provider does't define
 * Isample2DbSchemaMigrator implementation.
 */
public class Nullsample2DbSchemaMigrator : Isample2DbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
