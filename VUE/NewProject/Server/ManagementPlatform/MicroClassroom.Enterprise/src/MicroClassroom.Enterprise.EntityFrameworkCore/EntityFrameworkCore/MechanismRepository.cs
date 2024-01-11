using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MicroClassroom.Enterprise.EntityFrameworkCore;

public class MechanismRepository : EfCoreRepository<EnterpriseDbContext, Mechanism, Guid>, IMechanismRepository
{
    public MechanismRepository(IDbContextProvider<EnterpriseDbContext> dbContextProvider) : base(dbContextProvider)
    {

    }

    public async Task<Mechanism> GetSingleAsync()
    {
        return (await GetQueryableAsync()).SingleOrDefault();
    }

    public async Task<Mechanism> GetTenantAsync(Guid tenantId)
    {
        return (await GetQueryableAsync()).IncludeDetails().SingleOrDefault(m => m.TenantId == tenantId);
    }
}
