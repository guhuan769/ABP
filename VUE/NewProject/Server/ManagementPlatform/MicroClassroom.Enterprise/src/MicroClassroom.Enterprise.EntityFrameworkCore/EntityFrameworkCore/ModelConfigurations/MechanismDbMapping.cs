using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace MicroClassroom.Enterprise.EntityFrameworkCore.ModelConfigurations;

internal class MechanismDbMapping : IEntityTypeConfiguration<Mechanism>
{
    public void Configure(EntityTypeBuilder<Mechanism> builder)
    {
        // Primary Key
        builder.HasKey(m => m.Id).HasName("pk_mechanism_id");

        // Table & Column Mappings
        builder.ToTable("Mechanism");

        builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .IsRequired()
            .HasColumnName("Id")
            .HasComment("主键标识");

        builder.Property(t => t.Name).IsRequired().HasMaxLength(200)
            .HasColumnName("Name").HasComment("名称");
        builder.Property(t => t.Pinyin).IsRequired().HasMaxLength(50)
            .HasColumnName("Pinyin").HasComment("拼音或租户名");
        builder.Property(t => t.Image).IsRequired().HasMaxLength(500)
            .HasColumnName("Image").HasComment("图像");
        builder.Property(t => t.Slogo).IsRequired().HasMaxLength(200)
            .HasColumnName("Slogo").HasComment("Slogo");
        builder.Property(t => t.Introduce).HasMaxLength(500)
            .HasColumnName("Introduce").HasComment("介绍");
        builder.Property(t => t.Grade)
            .HasColumnName("Grade").HasComment("等级");
        builder.Property(t => t.About).HasMaxLength(2000)
            .HasColumnName("About").HasComment("关于我们");
        builder.Property(t => t.TenantId)
            .HasColumnName("TenantId").HasComment("租户id");

        builder.HasMany(c => c.Banners).WithOne().HasForeignKey(t => t.MechanismId)
            .HasConstraintName("fk_mechanism_banner_id").IsRequired();
        builder.HasMany(c => c.Teachers).WithOne().HasForeignKey(t => t.MechanismId)
            .HasConstraintName("fk_mechanism_teacher_id").IsRequired();
        builder.HasMany(t => t.Courses).WithOne().HasForeignKey(t => t.MechanismId)
            .HasConstraintName("fk_mechanism_course_id").IsRequired();

        builder.ConfigureByConvention();
        builder.ApplyObjectExtensionMappings();
    }
}
