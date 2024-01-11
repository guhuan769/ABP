using System;
using System.Collections.Generic;

namespace MicroClassroom.Enterprise;

public class MechanismDto
{
    public MechanismDto()
    {
        Courses = new List<CourseDto>();
        Teachers = new List<TeacherDto>();
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Pinyin { get; set; }

    public string Image { get; set; }

    public string Slogo { get; set; }

    public string Introduce { get; set; }

    public int? Grade { get; set; }

    public string About { get; set; }

    public virtual List<CourseDto> Courses { get; set; }

    public virtual List<TeacherDto> Teachers { get; set; }
}