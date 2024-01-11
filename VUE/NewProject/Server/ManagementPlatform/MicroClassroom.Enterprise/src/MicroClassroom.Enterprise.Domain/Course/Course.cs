using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace MicroClassroom.Enterprise;

/// <summary>
/// 课程
/// </summary>
public class Course : AggregateRoot<Guid>, IMultiTenant
{
    private Course()
    {
        CourseItems = new List<CourseItem>();
        CourseTeachers = new List<CourseTeacher>();
    }

    // 构造函数
    public Course(Guid id,
        Guid mechanismId,
        Guid categoryId,
        string name,
        string image,
        decimal price,
        bool? hasPay,
        string introduce,
        DateTime? startAt,
        DateTime? endAt,
        Guid? tenantId)
    {
        Id = id;
        MechanismId = mechanismId;
        CategoryId = categoryId;
        Name = name;
        Image = image;
        Price = price;
        HasPay = hasPay;
        Introduce = introduce;
        StartAt = startAt;
        EndAt = endAt;
        TenantId = tenantId;

        CourseItems = new List<CourseItem>();
        CourseTeachers = new List<CourseTeacher>();
    }

    public Guid MechanismId { get; private set; }

    public Guid CategoryId { get; private set; }

    public string Name { get; private set; }

    public string Image { get; private set; }

    public decimal Price { get; private set; }

    public bool? HasPay { get; private set; }

    public string Introduce { get; private set; }

    public DateTime? StartAt { get; private set; }

    public DateTime? EndAt { get; private set; }

    public Guid? TenantId { get; private set; }

    public virtual List<CourseItem> CourseItems { get; private set; }

    public virtual List<CourseTeacher> CourseTeachers { get; private set; }

    public void SetCourse(Guid categoryId,
        string name,
        string image,
        decimal price,
        bool? hasPay,
        string introduce,
        DateTime? startAt,
        DateTime? endAt)
    {
        CategoryId = categoryId;
        Name = name;
        Image = image;
        Price = price;
        HasPay = hasPay;
        Introduce = introduce;
        StartAt = startAt;
        EndAt = endAt;
    }

    public void SetCourseItem(IGuidGenerator guidGenerator,
        string title,
        int order,
        float duration,
        string video,
        DateTime startAt,
        DateTime endAt)
    {
        // 是否包含标题
        var courseItem = CourseItems.FirstOrDefault(t => t.Title == title);
        if (courseItem != null)
        {
            // 修改操作
            courseItem.SetValue(Id, title, order, duration, video, startAt, endAt);
        }
        else
        {
            // 插入操作 聚合根子对象(实体)添加对象 
            CourseItems.Add(new CourseItem(guidGenerator.Create(), Id, title, order, duration, video, startAt, endAt, TenantId));
        }
    }

    public bool AddCourseTeacher(IGuidGenerator guidGenerator, Guid teacherId)
    {
        var has = CourseTeachers.Any(ct => ct.TeacherId == teacherId);
        if (!has)
        {
            CourseTeachers.Add(new CourseTeacher(guidGenerator.Create(), Id, teacherId, TenantId));
        }

        return !has;
    }
}
