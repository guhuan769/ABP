using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MicroClassroom.Enterprise.EntityFrameworkCore;

public class TeacherRepository : EfCoreRepository<EnterpriseDbContext, Teacher, Guid>, ITeacherRepository
{
    public TeacherRepository(IDbContextProvider<EnterpriseDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public Task<List<Teacher>> GetByIds(Guid[] ids)
    {
        return GetListAsync(t => ids.Contains(t.Id));
    }
}
