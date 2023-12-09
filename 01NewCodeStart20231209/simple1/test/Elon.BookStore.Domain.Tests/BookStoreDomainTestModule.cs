using Elon.BookStore.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Elon.BookStore;

[DependsOn(
    typeof(BookStoreEntityFrameworkCoreTestModule)
    )]
public class BookStoreDomainTestModule : AbpModule
{

}
