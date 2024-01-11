using System;

namespace MicroClassroom.Enterprise;

public class CreateCourseInput
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public decimal Price { get; set; }

    public bool? HasPay { get; set; }

    public string Introduce { get; set; }

    public DateTime? StartAt { get; set; }

    public DateTime? EndAt { get; set; }
}
