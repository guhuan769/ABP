using System;
using System.Collections.Generic;

namespace MicroClassroom.Enterprise;

public class CourseDto
{
    public CourseDto()
    {
        CourseItems = new List<CourseItemDto>();
        Teachers = new List<TeacherDto>();
    }

    public Guid Id { get; set; }

    public Guid MechanismId { get; set; }

    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public decimal Price { get; set; }

    public bool? HasPay { get; set; }

    public string Introduce { get; set; }

    public DateTime? StartAt { get; set; }

    public DateTime? EndAt { get; set; }

    public virtual List<CourseItemDto> CourseItems { get; set; }

    public virtual List<TeacherDto> Teachers { get; set; }
}
