using MicroClassroom.Enterprise.Permissions;
using MicroClassroom.Shared;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace MicroClassroom.Enterprise;

public class TeacherAppService : EnterpriseAppService, ITeacherAppService
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly TeacherManager _teacherManager;
    private readonly CourseManager _courseManager;
    private readonly IDataFilter _dataFilter;

    public TeacherAppService(ITeacherRepository teacherRepository,
        TeacherManager teacherManager,
        CourseManager courseManager,
        IDataFilter dataFilter)
    {
        _teacherRepository = teacherRepository;
        _teacherManager = teacherManager;
        _courseManager = courseManager;
        _dataFilter = dataFilter;
    }

    public async Task<TeacherDto> CreateAsync(CreateTeacherInput input)
    {
        var teacher = await _teacherManager.CreateAsync(input.Name,
            input.Image,
            input.Introduce,
            CurrentTenant.Id);

        await _teacherRepository.InsertAsync(teacher);

        return ObjectMapper.Map<Teacher, TeacherDto>(teacher);
    }

    public async Task<TeacherDto> UpdateAsync(Guid id, UpdateTeacherInput input)
    {
        var teacher = await _teacherManager.UpdateAsync(id,
            input.Name,
            input.Image,
            input.Introduce);

        await _teacherRepository.UpdateAsync(teacher);

        return ObjectMapper.Map<Teacher, TeacherDto>(teacher);
    }


    [Authorize(EnterprisePermissions.Teachers.Delete)]
    public async Task<ApiResponse> RemoveAsync(Guid id)
    {
        var response = new ApiResponse<ApiResponse>();

        var has = await _courseManager.HasCourseTeacherForTeacher(id);
        if (has)
        {
            response.IsFailed("包含课程，不可删除");

            return response;
        }

        await _teacherRepository.DeleteAsync(id);

        return response;
    }

    public async Task<TeacherDto> GetAsync(Guid id)
    {
        var teacher = await _teacherRepository.GetAsync(id);

        return ObjectMapper.Map<Teacher, TeacherDto>(teacher);
    }

    public async Task<PagedResultDto<TeacherDto>> GetPagedListAsync(GetTeacherInput input)
    {
        var hasMultiTenant = CurrentTenant.Id.HasValue;
        using (var disposable = hasMultiTenant ?
            _dataFilter.Enable<IMultiTenant>() : _dataFilter.Disable<IMultiTenant>())
        {
            var count = await _teacherRepository.GetCountAsync();
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = "id desc";
            }

            var list = await _teacherRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);

            return new PagedResultDto<TeacherDto>(
                count,
                ObjectMapper.Map<List<Teacher>, List<TeacherDto>>(list)
            );
        }
    }
}
