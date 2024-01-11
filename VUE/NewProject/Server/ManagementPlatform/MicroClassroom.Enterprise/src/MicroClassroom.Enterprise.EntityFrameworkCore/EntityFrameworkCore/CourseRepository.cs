using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MicroClassroom.Enterprise.EntityFrameworkCore;

public class CourseRepository : EfCoreRepository<EnterpriseDbContext, Course, Guid>, ICourseRepository
{
    public CourseRepository(IDbContextProvider<EnterpriseDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<Course>> GetListAsync(Guid mechMechanismId)
    {
        return await GetListAsync(c => c.MechanismId == mechMechanismId);
    }
}
