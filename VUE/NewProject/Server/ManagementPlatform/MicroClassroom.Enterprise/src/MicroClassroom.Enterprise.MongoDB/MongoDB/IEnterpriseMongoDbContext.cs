using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MicroClassroom.Enterprise.MongoDB;

[ConnectionStringName(EnterpriseDbProperties.ConnectionStringName)]
public interface IEnterpriseMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
