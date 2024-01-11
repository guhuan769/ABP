using System;

namespace MicroClassroom.Enterprise;

public class TeacherDto
{
    public Guid Id { get; set; }

    public Guid MechanismId { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public string Introduce { get; set; }
}
