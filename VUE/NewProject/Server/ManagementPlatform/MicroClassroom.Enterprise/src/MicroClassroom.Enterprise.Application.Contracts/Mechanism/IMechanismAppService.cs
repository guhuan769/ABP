using MicroClassroom.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MicroClassroom.Enterprise;

public interface IMechanismAppService : IApplicationService
{
    Task<MechanismDto> GetAsync(Guid id);

    Task<MechanismDto> GetTenantAsync(Guid tenantId);

    Task<MechanismDto> CreateAsync(CreateMechanismInput input);

    Task<MechanismDto> UpdateAsync(Guid id, UpdateMechanismInput input);

    Task<ApiResponse> RemoveAsync(Guid id);

    Task<PagedResultDto<MechanismDto>> GetPagedListAsync(GetMechanismsInput input);
}
