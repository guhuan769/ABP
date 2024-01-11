using System;

namespace MicroClassroom.Enterprise;

public class BannerDto
{
    public Guid Id { get; set; }

    public Guid MechanismId { get; set; }

    public string Title { get; set; }

    public string Image { get; set; }
}
