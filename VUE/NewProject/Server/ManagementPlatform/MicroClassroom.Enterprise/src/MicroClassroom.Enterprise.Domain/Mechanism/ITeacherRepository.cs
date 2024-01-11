using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MicroClassroom.Enterprise;

public interface ITeacherRepository : IRepository<Teacher, Guid>
{
    Task<List<Teacher>> GetByIds(Guid[] ids);
}
