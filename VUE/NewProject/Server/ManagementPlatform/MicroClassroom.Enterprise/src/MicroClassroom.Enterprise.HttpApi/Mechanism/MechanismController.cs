using MicroClassroom.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace MicroClassroom.Enterprise;

[Area(EnterpriseRemoteServiceConsts.ModuleName)]
[RemoteService(Name = EnterpriseRemoteServiceConsts.RemoteServiceName)]
[Route("api/enterprise/mechanism")]
public class MechanismController : EnterpriseController, IMechanismAppService
{
    private readonly IMechanismAppService _mechanismAppService;

    public MechanismController(IMechanismAppService mechanismAppService)
    {
        _mechanismAppService = mechanismAppService;
    }

    [HttpPost("create")]
    public async Task<MechanismDto> CreateAsync(CreateMechanismInput input)
    {
        return await _mechanismAppService.CreateAsync(input);
    }

    [HttpDelete("remove")]
    public async Task<ApiResponse> RemoveAsync(Guid id)
    {
        return await _mechanismAppService.RemoveAsync(id);
    }

    [HttpPost("update/{id}")]
    public async Task<MechanismDto> UpdateAsync(Guid id, UpdateMechanismInput input)
    {
        return await _mechanismAppService.UpdateAsync(id, input);
    }

    [HttpGet("{id}")]
    public async Task<MechanismDto> GetAsync(Guid id)
    {
        return await _mechanismAppService.GetAsync(id);
    }

    [HttpGet("tenant/{tenantId}")]
    public async Task<MechanismDto> GetTenantAsync(Guid tenantId)
    {
        return await _mechanismAppService.GetTenantAsync(tenantId);
    }

    [HttpGet("paged-list")]
    public async Task<PagedResultDto<MechanismDto>> GetPagedListAsync(GetMechanismsInput input)
    {
        return await _mechanismAppService.GetPagedListAsync(input);
    }
}
