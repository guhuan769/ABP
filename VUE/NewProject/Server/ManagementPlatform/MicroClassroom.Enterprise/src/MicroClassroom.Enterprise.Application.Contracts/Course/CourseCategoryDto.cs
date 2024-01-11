using System;

namespace MicroClassroom.Enterprise;

public class CourseCategoryDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int? Status { get; set; }
}
