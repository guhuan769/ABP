using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MicroClassroom.Enterprise.EntityFrameworkCore;

[ConnectionStringName(EnterpriseDbProperties.ConnectionStringName)]
public class EnterpriseDbContext : AbpDbContext<EnterpriseDbContext>, IEnterpriseDbContext
{
    public DbSet<Mechanism> Mechanisms { get; set; }

    public DbSet<Banner> Banners { get; set; }

    public DbSet<Teacher> Teachers { get; set; }

    public DbSet<CourseCategory> CourseCategories { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<CourseItem> CourseItems { get; set; }

    public DbSet<CourseTeacher> CourseTeacher { get; set; }

    public EnterpriseDbContext(DbContextOptions<EnterpriseDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureEnterprise();
    }
}
