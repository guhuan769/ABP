using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MicroClassroom.Enterprise.EntityFrameworkCore;

public class EnterpriseHttpApiHostMigrationsDbContext : AbpDbContext<EnterpriseHttpApiHostMigrationsDbContext>
{
    public EnterpriseHttpApiHostMigrationsDbContext(DbContextOptions<EnterpriseHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureEnterprise();
    }
}
