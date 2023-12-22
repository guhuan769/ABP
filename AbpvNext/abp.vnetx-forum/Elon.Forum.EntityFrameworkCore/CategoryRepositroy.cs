using Elon.Forum.Domain.Entities;
using Elon.Forum.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Elon.Forum.EntityFrameworkCore;

public class CategoryRepositroy : EfCoreRepository<ForumDbContext, CategoryEntity, long>, ICategoryRepository
{
    public CategoryRepositroy(IDbContextProvider<ForumDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<CategoryEntity>> GetListOfIdArrayAsync(long[] userIds)
    {
        return (await GetQueryableAsync()).Where(t => userIds.Contains(t.Id)).ToList();
    }
}