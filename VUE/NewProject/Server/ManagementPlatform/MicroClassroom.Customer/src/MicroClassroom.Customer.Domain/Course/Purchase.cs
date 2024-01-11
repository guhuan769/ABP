using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace MicroClassroom.Customer;

public class Purchase : Entity<Guid>, IMultiTenant
{
    private Purchase() { }

    public Purchase(Guid courseId, Guid userId, decimal price, bool? isPay, decimal? discount, decimal? payIn, DateTime createAt, Guid? tenantId)
    {
        CourseId = courseId;
        UserId = userId;
        Price = price;
        IsPay = isPay;
        Discount = discount;
        PayIn = payIn;
        CreateAt = createAt;
        TenantId = tenantId;
    }

    public Guid CourseId { get; private set; }

    public Guid UserId { get; private set; }

    public decimal Price { get; private set; }

    public bool? IsPay { get; private set; }

    public decimal? Discount { get; private set; }

    public decimal? PayIn { get; private set; }

    public DateTime CreateAt { get; private set; }

    public Guid? TenantId { get; private set; }
}
