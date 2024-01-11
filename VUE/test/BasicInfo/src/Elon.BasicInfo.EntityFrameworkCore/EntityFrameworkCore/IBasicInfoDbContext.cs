using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Elon.BasicInfo.EntityFrameworkCore;

[ConnectionStringName(BasicInfoDbProperties.ConnectionStringName)]
public interface IBasicInfoDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
