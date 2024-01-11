using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace MicroClassroom.Customer.EntityFrameworkCore.ModelConfigurations;

internal class PurchaseDbMapping : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        // Primary Key
        builder.HasKey(m => m.Id).HasName("pk_purchase_id");

        // Table & Column Mappings
        builder.ToTable("Purchase");

        builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .IsRequired()
            .HasColumnName("Id")
            .HasComment("主键标识");

        builder.Property(t => t.CourseId).IsRequired()
            .HasColumnName("CourseId").HasComment("课程id");
        builder.Property(t => t.UserId).IsRequired()
            .HasColumnName("UserId").HasComment("用户id");
        builder.Property(t => t.IsPay)
            .HasColumnName("IsPay").HasComment("实付付款");
        builder.Property(t => t.Price).IsRequired()
            .HasColumnName("Price").HasPrecision(9, 2).HasComment("课程价格");
        builder.Property(t => t.Discount)
            .HasColumnName("Discount").HasPrecision(9, 2).HasComment("优惠金额");
        builder.Property(t => t.PayIn)
            .HasColumnName("PayIn").HasPrecision(9, 2).HasComment("实付金额");
        builder.Property(t => t.CreateAt)
            .HasColumnName("CreateAt").HasComment("评论时间");
        builder.Property(t => t.TenantId)
            .HasColumnName("TenantId").HasComment("租户id");

        builder.ConfigureByConvention();
        builder.ApplyObjectExtensionMappings();
    }
}
