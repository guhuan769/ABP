using MicroClassroom.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MicroClassroom.Enterprise;

public interface ITeacherAppService : IApplicationService
{
    Task<TeacherDto> GetAsync(Guid id);

    Task<TeacherDto> CreateAsync(CreateTeacherInput input);

    Task<TeacherDto> UpdateAsync(Guid id, UpdateTeacherInput input);

    Task<ApiResponse> RemoveAsync(Guid id);

    Task<PagedResultDto<TeacherDto>> GetPagedListAsync(GetTeacherInput input);
}
