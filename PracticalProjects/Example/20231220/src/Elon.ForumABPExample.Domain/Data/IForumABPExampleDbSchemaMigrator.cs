using System.Threading.Tasks;

namespace Elon.ForumABPExample.Data;

public interface IForumABPExampleDbSchemaMigrator
{
    Task MigrateAsync();
}
