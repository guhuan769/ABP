using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace ManagementPlatform.Company.EntityFrameworkCore;

[ConnectionStringName(CompanyDbProperties.ConnectionStringName)]
public interface ICompanyDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
