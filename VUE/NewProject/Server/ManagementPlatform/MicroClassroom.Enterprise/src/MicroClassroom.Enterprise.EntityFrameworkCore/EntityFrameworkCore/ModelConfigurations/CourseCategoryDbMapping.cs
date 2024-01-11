using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace MicroClassroom.Enterprise.EntityFrameworkCore.ModelConfigurations;

internal class CourseCategoryDbMapping : IEntityTypeConfiguration<CourseCategory>
{
    public void Configure(EntityTypeBuilder<CourseCategory> builder)
    {
        // Primary Key
        builder.HasKey(m => m.Id).HasName("pk_course_category_id");

        // Table & Column Mappings
        builder.ToTable("CourseCategory");

        builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .IsRequired()
            .HasColumnName("Id")
            .HasComment("主键标识");

        builder.Property(t => t.Name).IsRequired().HasMaxLength(50)
            .HasColumnName("Name").HasComment("分类名");
        builder.Property(t => t.Status)
            .HasColumnName("Status").HasComment("分类状态");
        builder.Property(t => t.TenantId)
            .HasColumnName("TenantId").HasComment("租户id");

        builder.HasMany(c => c.Courses).WithOne().HasForeignKey(t => t.CategoryId)
            .HasConstraintName("fk_category_course_id").IsRequired();

        builder.ConfigureByConvention();
        builder.ApplyObjectExtensionMappings();
    }
}
