using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace MicroClassroom.Enterprise;

/// <summary>
/// 课程分类
/// </summary>
public class CourseCategory : Entity<Guid>, IMultiTenant
{
    private CourseCategory() { }

    public CourseCategory(Guid id, string name, int? status, Guid? tenantId)
    {
        Id = id;
        Name = name;
        Status = status;
        TenantId = tenantId;
    }

    public string Name { get; private set; }

    public int? Status { get; private set; }

    public Guid? TenantId { get; private set; }

    public virtual List<Course> Courses { get; private set; }

    public void SetValue(string name, int? status)
    {
        Name = name;
        Status = status;
    }
}
