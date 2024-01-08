using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Elon.BasicInfo.EntityFrameworkCore;

public class BasicInfoHttpApiHostMigrationsDbContext : AbpDbContext<BasicInfoHttpApiHostMigrationsDbContext>
{
    public BasicInfoHttpApiHostMigrationsDbContext(DbContextOptions<BasicInfoHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureBasicInfo();
    }
}
