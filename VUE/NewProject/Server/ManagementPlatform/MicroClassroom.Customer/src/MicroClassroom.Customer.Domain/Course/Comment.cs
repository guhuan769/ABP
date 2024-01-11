using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace MicroClassroom.Customer;

public class Comment : Entity<Guid>, IMultiTenant
{
    private Comment() { }

    public Comment(Guid courseId, Guid userId, string content, int star, DateTime createAt, Guid? tenantId)
    {
        CourseId = courseId;
        UserId = userId;
        Content = content;
        Star = star;
        CreateAt = createAt;
        TenantId = tenantId;
    }

    public Guid CourseId { get; private set; }

    public Guid UserId { get; private set; }

    public string Content { get; private set; }

    public int Star { get; private set; }

    public DateTime CreateAt { get; private set; }

    public Guid? TenantId { get; private set; }
}
