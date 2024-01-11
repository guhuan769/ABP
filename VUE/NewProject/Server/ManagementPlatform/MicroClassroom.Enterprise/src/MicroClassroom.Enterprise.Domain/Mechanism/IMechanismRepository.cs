using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MicroClassroom.Enterprise;

public interface IMechanismRepository : IRepository<Mechanism, Guid>
{
    Task<Mechanism> GetSingleAsync();

    Task<Mechanism> GetTenantAsync(Guid tenantId);
}
