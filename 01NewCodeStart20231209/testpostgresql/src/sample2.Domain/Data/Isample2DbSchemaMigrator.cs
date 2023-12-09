using System.Threading.Tasks;

namespace sample2.Data;

public interface Isample2DbSchemaMigrator
{
    Task MigrateAsync();
}
