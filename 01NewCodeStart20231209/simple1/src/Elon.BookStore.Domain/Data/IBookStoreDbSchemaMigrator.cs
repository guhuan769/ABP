using System.Threading.Tasks;

namespace Elon.BookStore.Data;

public interface IBookStoreDbSchemaMigrator
{
    Task MigrateAsync();
}
