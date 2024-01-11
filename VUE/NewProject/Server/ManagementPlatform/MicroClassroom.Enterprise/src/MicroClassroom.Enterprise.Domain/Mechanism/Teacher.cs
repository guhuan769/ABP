using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace MicroClassroom.Enterprise;

/// <summary>
/// 教师
/// </summary>
public class Teacher : Entity<Guid>, IMultiTenant
{
    private Teacher() { }

    public Teacher(Guid id, Guid mechanismId, string name, string image, string introduce, Guid? tenantId)
    {
        Id = id;
        MechanismId = mechanismId;
        Name = name;
        Image = image;
        Introduce = introduce;
        TenantId = tenantId;
    }

    public Guid MechanismId { get; private set; }

    public string Name { get; private set; }

    public string Image { get; private set; }

    public string Introduce { get; private set; }

    public Guid? TenantId { get; private set; }

    //public virtual List<CourseTeacher> CourseTeachers { get; private set; }

    public void SetValue(Guid mechanismId, string name, string image, string introduce)
    {
        MechanismId = mechanismId;
        Name = name;
        Image = image;
        Introduce = introduce;
    }

    public void SetValue(string name, string image, string introduce)
    {
        Name = name;
        Image = image;
        Introduce = introduce;
    }
}
