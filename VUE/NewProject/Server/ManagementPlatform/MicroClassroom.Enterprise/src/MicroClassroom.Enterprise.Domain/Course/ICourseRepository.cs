using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MicroClassroom.Enterprise;

public interface ICourseRepository : IRepository<Course, Guid>
{
    Task<List<Course>> GetListAsync(Guid mechMechanismId);
}
