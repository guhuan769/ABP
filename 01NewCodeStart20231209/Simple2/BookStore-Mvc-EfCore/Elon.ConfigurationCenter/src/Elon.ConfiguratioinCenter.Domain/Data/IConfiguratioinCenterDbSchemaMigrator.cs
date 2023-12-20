using System.Threading.Tasks;

namespace Elon.ConfiguratioinCenter.Data;

public interface IConfiguratioinCenterDbSchemaMigrator
{
    Task MigrateAsync();
}
