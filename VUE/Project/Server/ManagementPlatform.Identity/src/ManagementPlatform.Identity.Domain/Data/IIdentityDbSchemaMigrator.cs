using System.Threading.Tasks;

namespace ManagementPlatform.Identity.Data;

public interface IIdentityDbSchemaMigrator
{
    Task MigrateAsync();
}
