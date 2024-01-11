using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace ManagementPlatform.Company.EntityFrameworkCore;

[ConnectionStringName(CompanyDbProperties.ConnectionStringName)]
public class CompanyDbContext : AbpDbContext<CompanyDbContext>, ICompanyDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureCompany();
    }
}
