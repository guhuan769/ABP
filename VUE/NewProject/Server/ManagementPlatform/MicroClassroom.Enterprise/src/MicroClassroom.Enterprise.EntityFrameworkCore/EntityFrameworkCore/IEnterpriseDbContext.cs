using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MicroClassroom.Enterprise.EntityFrameworkCore;

[ConnectionStringName(EnterpriseDbProperties.ConnectionStringName)]
public interface IEnterpriseDbContext : IEfCoreDbContext
{
    DbSet<Mechanism> Mechanisms { get; }

    DbSet<Banner> Banners { get; }

    DbSet<Teacher> Teachers { get; }

    DbSet<CourseCategory> CourseCategories { get; }

    DbSet<Course> Courses { get; }

    DbSet<CourseItem> CourseItems { get; }

    DbSet<CourseTeacher> CourseTeacher { get; }
}
