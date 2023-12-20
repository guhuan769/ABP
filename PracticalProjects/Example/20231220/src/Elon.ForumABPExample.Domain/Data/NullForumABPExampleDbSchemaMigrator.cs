using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Elon.ForumABPExample.Data;

/* This is used if database provider does't define
 * IForumABPExampleDbSchemaMigrator implementation.
 */
public class NullForumABPExampleDbSchemaMigrator : IForumABPExampleDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
