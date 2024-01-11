using MicroClassroom.Enterprise.EntityFrameworkCore.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace MicroClassroom.Enterprise.EntityFrameworkCore;

public static class EnterpriseDbContextModelCreatingExtensions
{
    public static void ConfigureEnterprise(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.ApplyConfiguration(new MechanismDbMapping())
            .ApplyConfiguration(new BannerDbMapping())
            .ApplyConfiguration(new TeacherDbMapping())
            .ApplyConfiguration(new CourseCategoryDbMapping())
            .ApplyConfiguration(new CourseDbMapping())
            .ApplyConfiguration(new CourseItemDbMapping())
            .ApplyConfiguration(new CourseTeacherDbMapping());
    }
}
