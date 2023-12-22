using Volo.Abp.Domain.Entities;

namespace Elon.Forum.Domain;

public class BaseEntity : Entity<long>
{
    public DateTime CreationTime { get; set; } = DateTime.Now;
}