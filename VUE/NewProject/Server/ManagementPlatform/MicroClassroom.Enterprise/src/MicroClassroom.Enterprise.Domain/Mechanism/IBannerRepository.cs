using System;
using Volo.Abp.Domain.Repositories;

namespace MicroClassroom.Enterprise;

public interface IBannerRepository : IRepository<Banner, Guid>
{
}
