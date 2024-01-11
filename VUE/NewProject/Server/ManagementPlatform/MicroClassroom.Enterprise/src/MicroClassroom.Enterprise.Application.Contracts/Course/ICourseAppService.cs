using MicroClassroom.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MicroClassroom.Enterprise;

public interface ICourseAppService : IApplicationService
{
    Task<CourseDto> CreateCourseAsync(CreateCourseInput input);

    Task<CourseDto> UpdateCourseAsync(Guid id, UpdateCourseInput input);

    Task<CourseCategoryDto> CreateCategoryAsync(CourseCategoryInput input);

    Task UpdateCourseItemAsync(CourseItemInput input);

    Task CreateCourseTeacherAsync(Guid id, Guid teacherId);

    Task<ApiResponse> RemoveAsync(Guid id);

    Task<CourseDto> GetAsync(Guid id);

    Task<ListResultDto<CourseCategoryDto>> GetCategoryAsync();

    Task<ListResultDto<CourseDto>> GetListAsync(Guid mId);

    Task<ListResultDto<CourseDto>> GetListAsync();

    Task<PagedResultDto<CourseDto>> GetPagedListAsync(GetCoursesInput input);
}
