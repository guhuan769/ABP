using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ManagementPlatform.Company.EntityFrameworkCore;

public class CompanyHttpApiHostMigrationsDbContext : AbpDbContext<CompanyHttpApiHostMigrationsDbContext>
{
    public CompanyHttpApiHostMigrationsDbContext(DbContextOptions<CompanyHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureCompany();
    }
}
