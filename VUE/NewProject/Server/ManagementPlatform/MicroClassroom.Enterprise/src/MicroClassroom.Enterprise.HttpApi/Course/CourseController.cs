using MicroClassroom.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace MicroClassroom.Enterprise;

[Area(EnterpriseRemoteServiceConsts.ModuleName)]
[RemoteService(Name = EnterpriseRemoteServiceConsts.RemoteServiceName)]
[Route("api/enterprise/course")]
public class CourseController : EnterpriseController, ICourseAppService
{
    private readonly ICourseAppService _courseAppService;

    public CourseController(ICourseAppService courseAppService)
    {
        _courseAppService = courseAppService;
    }

    [HttpPost("create-category")]
    public async Task<CourseCategoryDto> CreateCategoryAsync(CourseCategoryInput input)
    {
        return await _courseAppService.CreateCategoryAsync(input);
    }

    [HttpPost("create-course")]
    public async Task<CourseDto> CreateCourseAsync(CreateCourseInput input)
    {
        return await _courseAppService.CreateCourseAsync(input);
    }

    [HttpPost("update-course/{id}")]
    public async Task<CourseDto> UpdateCourseAsync(Guid id, UpdateCourseInput input)
    {
        return await _courseAppService.UpdateCourseAsync(id, input);
    }

    [HttpPost("{id}/course-teacher/{teacherId}")]
    public async Task CreateCourseTeacherAsync(Guid id, Guid teacherId)
    {
        await _courseAppService.CreateCourseTeacherAsync(id, teacherId);
    }

    [HttpGet("{id}")]
    public async Task<CourseDto> GetAsync(Guid id)
    {
        return await _courseAppService.GetAsync(id);
    }

    [HttpGet("category")]
    public async Task<ListResultDto<CourseCategoryDto>> GetCategoryAsync()
    {
        return await _courseAppService.GetCategoryAsync();
    }

    [HttpGet("list/{mechMechanismId}")]
    public async Task<ListResultDto<CourseDto>> GetListAsync(Guid mechMechanismId)
    {
        return await _courseAppService.GetListAsync(mechMechanismId);
    }

    [HttpGet("list")]
    public async Task<ListResultDto<CourseDto>> GetListAsync()
    {
        return await _courseAppService.GetListAsync();
    }

    [HttpGet("page-list")]
    public async Task<PagedResultDto<CourseDto>> GetPagedListAsync(GetCoursesInput input)
    {
        return await _courseAppService.GetPagedListAsync(input);
    }

    [HttpPut("course-item")]
    public async Task UpdateCourseItemAsync(CourseItemInput input)
    {
        await _courseAppService.UpdateCourseItemAsync(input);
    }

    [HttpDelete("remove")]
    public async Task<ApiResponse> RemoveAsync(Guid id)
    {
        return await _courseAppService.RemoveAsync(id);
    }
}
