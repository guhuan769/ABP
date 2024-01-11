using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace MicroClassroom.Enterprise;

public class CourseTeacher : Entity<Guid>, IMultiTenant
{
    private CourseTeacher() { }

    public CourseTeacher(Guid id , Guid courseId, Guid teacherId, Guid? tenantId)
    {
        Id = id;
        CourseId = courseId;
        TeacherId = teacherId;
        TenantId = tenantId;
    }

    public Guid CourseId { get; private set; }

    public Guid TeacherId { get; private set; }

    public Guid? TenantId { get; private set; }
}
