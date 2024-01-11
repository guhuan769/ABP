using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Elon.BasicInfo.MongoDB;

[ConnectionStringName(BasicInfoDbProperties.ConnectionStringName)]
public interface IBasicInfoMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
