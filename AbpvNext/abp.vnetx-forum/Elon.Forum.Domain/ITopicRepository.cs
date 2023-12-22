using Volo.Abp.Domain.Repositories;
using Elon.Forum.Domain.Entities;
using Zhaoxi.Forum.Domain.Entities;

namespace Elon.Forum.Domain.Repositories;

public interface ITopicRepository : IRepository<TopicEntity, long>
{
    Task<List<TopicEntity>> GetHotTopicsAsync(long categoryId, int times = 5);

    Task<Tuple<int, List<TopicEntity>>> GetTopicByCategory(long categoryId, int PageIndex, int pageSize);

}