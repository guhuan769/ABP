using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace MicroClassroom.Enterprise.EntityFrameworkCore.ModelConfigurations;

internal class CourseTeacherDbMapping : IEntityTypeConfiguration<CourseTeacher>
{
    public void Configure(EntityTypeBuilder<CourseTeacher> builder)
    {
        // Primary Key
        builder.HasKey(m => m.Id).HasName("pk_course_teacher_id");

        // Table & Column Mappings
        builder.ToTable("CourseTeacher");

        builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .IsRequired()
            .HasColumnName("Id")
            .HasComment("主键标识");

        builder.Property(t => t.CourseId).IsRequired()
            .HasColumnName("CourseId").HasComment("课程id");
        builder.Property(t => t.TeacherId).IsRequired()
            .HasColumnName("TeacherId").HasComment("教师id");
        builder.Property(t => t.TenantId)
            .HasColumnName("TenantId").HasComment("租户id");

        builder.ConfigureByConvention();
        builder.ApplyObjectExtensionMappings();
    }
}
