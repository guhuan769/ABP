using System.Threading.Tasks;

namespace Elon.Identity.Data;

public interface IIdentityDbSchemaMigrator
{
    Task MigrateAsync();
}
