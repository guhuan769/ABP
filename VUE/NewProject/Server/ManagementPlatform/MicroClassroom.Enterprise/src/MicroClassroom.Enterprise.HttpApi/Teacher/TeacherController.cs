using MicroClassroom.Enterprise.Permissions;
using MicroClassroom.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace MicroClassroom.Enterprise;

[Area(EnterpriseRemoteServiceConsts.ModuleName)]
[RemoteService(Name = EnterpriseRemoteServiceConsts.RemoteServiceName)]
[Route("api/enterprise/teacher")]
public class TeacherController : EnterpriseController, ITeacherAppService
{
    private readonly ITeacherAppService _teacherAppService;

    public TeacherController(ITeacherAppService teacherAppService)
    {
        _teacherAppService = teacherAppService;
    }

    [HttpPost("create")]
    public async Task<TeacherDto> CreateAsync(CreateTeacherInput input)
    {
        return await _teacherAppService.CreateAsync(input);
    }

    [HttpPost("update/{id}")]
    public async Task<TeacherDto> UpdateAsync(Guid id, UpdateTeacherInput input)
    {
        return await _teacherAppService.UpdateAsync(id, input);
    }

    [HttpGet("{id}")]
    public async Task<TeacherDto> GetAsync(Guid id)
    {
        return await _teacherAppService.GetAsync(id);
    }

    [HttpGet("page-list")]
    public async Task<PagedResultDto<TeacherDto>> GetPagedListAsync(GetTeacherInput input)
    {
        return await _teacherAppService.GetPagedListAsync(input);
    }

    [HttpDelete("remove")]
    [Authorize(EnterprisePermissions.Teachers.Delete)]
    public async Task<ApiResponse> RemoveAsync(Guid id)
    {
        return await _teacherAppService.RemoveAsync(id);
    }
}
