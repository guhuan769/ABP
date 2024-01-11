using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace MicroClassroom.Enterprise;

/// <summary>
/// 机构
/// </summary>
public class Mechanism : AggregateRoot<Guid>, IMultiTenant
{
    private Mechanism()
    {
        Banners = new List<Banner>();
        Teachers = new List<Teacher>();
        Courses = new List<Course>();
    }

    public Mechanism(Guid id, string name, string pinyin, string image, string slogo, string introduce, int? grade, string about, Guid? tenantId)
    {
        Id = id;
        Name = name;
        Pinyin = pinyin;
        Image = image;
        Slogo = slogo;
        Introduce = introduce;
        Grade = grade;
        About = about;
        TenantId = tenantId;

        Banners = new List<Banner>();
        Teachers = new List<Teacher>();
        Courses = new List<Course>();
    }

    public string Name { get; private set; }

    public string Pinyin { get; private set; }

    public string Image { get; private set; }

    public string Slogo { get; private set; }

    public string Introduce { get; private set; }

    public int? Grade { get; private set; }

    public string About { get; private set; }

    public Guid? TenantId { get; private set; }

    public virtual List<Banner> Banners { get; private set; }

    public virtual List<Teacher> Teachers { get; private set; }

    public virtual List<Course> Courses { get; set; }

    public void SetValue(string name, 
        string pinyin, 
        string image, 
        string slogo, 
        string introduce, 
        int? grade, 
        string about)
    {
        Name = name;
        Pinyin = pinyin;
        Image = image;
        Slogo = slogo;
        Introduce = introduce;
        Grade = grade;
        About = about;
    }

    public void SetTeacher(IGuidGenerator guidGenerator, string name, string image, string introduce)
    {
        var teacher = Teachers.FirstOrDefault(t => t.Name == name);
        if (teacher != null)
        {
            teacher.SetValue(Id, name, image, introduce);
        }
        else
        {
            Teachers.Add(new Teacher(guidGenerator.Create(), Id, name, image, introduce, TenantId));
        }
    }

    public void SetBanner(IGuidGenerator guidGenerator, string title, string image)
    {
        var banner = Banners.FirstOrDefault(b => b.Title == title);
        if (banner != null)
        {
            banner.SetValue(Id, title, image);
        }
        else
        {
            Banners.Add(new Banner(guidGenerator.Create(), Id, title, image, TenantId));
        }
    }
}
