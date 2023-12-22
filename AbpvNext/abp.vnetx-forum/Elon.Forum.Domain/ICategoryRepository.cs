using Volo.Abp.Domain.Repositories;
using Elon.Forum.Domain.Entities;

namespace Elon.Forum.Domain.Repositories;

public interface ICategoryRepository : IRepository<CategoryEntity, long>
{
    Task<List<CategoryEntity>> GetListOfIdArrayAsync(long[] userIds);
}