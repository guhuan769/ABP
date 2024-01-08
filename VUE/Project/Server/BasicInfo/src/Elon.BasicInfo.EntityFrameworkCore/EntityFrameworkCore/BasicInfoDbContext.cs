using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Elon.BasicInfo.EntityFrameworkCore;

[ConnectionStringName(BasicInfoDbProperties.ConnectionStringName)]
public class BasicInfoDbContext : AbpDbContext<BasicInfoDbContext>, IBasicInfoDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public BasicInfoDbContext(DbContextOptions<BasicInfoDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureBasicInfo();
    }
}
