using System;

namespace MicroClassroom.Enterprise;

public class CourseItemDto
{
    public Guid Id { get; set; }

    public Guid CourseId { get; set; }

    public string Title { get; set; }

    public int Order { get; set; }

    public float Duration { get; set; }

    public string Video { get; set; }

    public DateTime StartAt { get; set; }

    public DateTime EndAt { get; set; }
}
