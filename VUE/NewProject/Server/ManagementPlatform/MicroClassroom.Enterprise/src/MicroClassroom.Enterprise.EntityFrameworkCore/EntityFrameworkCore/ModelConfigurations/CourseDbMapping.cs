using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace MicroClassroom.Enterprise.EntityFrameworkCore.ModelConfigurations;

internal class CourseDbMapping : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        // Primary Key
        builder.HasKey(m => m.Id).HasName("pk_course_id");

        // Table & Column Mappings
        builder.ToTable("Course");

        builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .IsRequired()
            .HasColumnName("Id")
            .HasComment("主键标识");

        builder.Property(t => t.CategoryId).IsRequired()
            .HasColumnName("CategoryId").HasComment("课程分类");
        builder.Property(t => t.Name).IsRequired().HasMaxLength(50)
            .HasColumnName("Name").HasComment("名称");
        builder.Property(t => t.Image).IsRequired().HasMaxLength(500)
            .HasColumnName("Image").HasComment("图像");
        builder.Property(t => t.Price).IsRequired()
            .HasColumnName("Price").HasPrecision(9, 2).HasComment("课程价格");
        builder.Property(t => t.HasPay)
            .HasColumnName("HasPay").HasComment("是否付费");
        builder.Property(t => t.Introduce).HasMaxLength(500)
            .HasColumnName("Introduce").HasComment("介绍");
        builder.Property(t => t.StartAt)
            .HasColumnName("StartAt").HasComment("开始时间");
        builder.Property(t => t.EndAt)
            .HasColumnName("EndAt").HasComment("结束时间");
        builder.Property(t => t.TenantId)
            .HasColumnName("TenantId").HasComment("租户id");

        builder.HasMany(c => c.CourseItems).WithOne().HasForeignKey(t => t.CourseId)
            .HasConstraintName("fk_course_item_id").IsRequired();
        //builder.HasMany(c => c.CourseTeachers).WithOne().HasForeignKey(t => t.CourseId)
        //    .HasConstraintName("fk_course_teacher_id").IsRequired();

        builder.ConfigureByConvention();
        builder.ApplyObjectExtensionMappings();
    }
}
