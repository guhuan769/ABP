using System.Threading.Tasks;

namespace MicroClassroom.Identity.Data;

public interface IIdentityDbSchemaMigrator
{
    Task MigrateAsync();
}
