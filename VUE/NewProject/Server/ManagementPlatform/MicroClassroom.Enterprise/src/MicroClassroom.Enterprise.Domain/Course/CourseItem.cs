using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace MicroClassroom.Enterprise;

public class CourseItem : Entity<Guid>, IMultiTenant
{
    private CourseItem() { }

    public CourseItem(Guid id,
        Guid courseId,
        string title,
        int order,
        float duration,
        string video,
        DateTime startAt,
        DateTime endAt,
        Guid? tenantId)
    {
        Id = id;
        CourseId = courseId;
        Title = title;
        Order = order;
        Duration = duration;
        Video = video;
        StartAt = startAt;
        EndAt = endAt;
        TenantId = tenantId;
    }

    public Guid CourseId { get; private set; }

    public string Title { get; private set; }

    public int Order { get; private set; }

    public float Duration { get; private set; }

    public string Video { get; private set; }

    public DateTime StartAt { get; private set; }

    public DateTime EndAt { get; private set; }

    public Guid? TenantId { get; private set; }

    public void SetValue(Guid courseId,
        string title,
        int order,
        float duration,
        string video,
        DateTime startAt,
        DateTime endAt)
    {
        CourseId = courseId;
        Title = title;
        Order = order;
        Duration= duration;
        Video = video;
        StartAt = startAt;
        EndAt = endAt;
    }
}
