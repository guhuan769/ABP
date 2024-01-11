using MicroClassroom.Enterprise.Permissions;
using MicroClassroom.Shared;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace MicroClassroom.Enterprise;

public class CourseAppService : EnterpriseAppService, ICourseAppService
{
    private readonly CourseManager _courseManager;
    private readonly ICourseRepository _courseRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IRepository<CourseCategory> _courseCategoryRepository;
    private readonly IDataFilter _dataFilter;

    public CourseAppService(CourseManager courseManager,
        ICourseRepository courseRepository,
        ITeacherRepository teacherRepository,
        IRepository<CourseCategory> courseCategoryRepository,
        IDataFilter dataFilter)
    {
        _courseManager = courseManager;
        _courseRepository = courseRepository;
        _teacherRepository = teacherRepository;
        _courseCategoryRepository = courseCategoryRepository;
        _dataFilter = dataFilter;
    }

    public async Task<CourseDto> CreateCourseAsync(CreateCourseInput input)
    {
        var course = await _courseManager.CreateAsync(input.CategoryId,
            input.Name,
            input.Image,
            input.Price,
            input.HasPay,
            input.Introduce,
            input.StartAt,
            input.EndAt,
            CurrentTenant.Id);

        // 仓储做数据保存
        await _courseRepository.InsertAsync(course);

        return ObjectMapper.Map<Course, CourseDto>(course);
    }

    public async Task<CourseDto> UpdateCourseAsync(Guid id, UpdateCourseInput input)
    {
        var course = await _courseManager.UpdateAsync(id,
            input.CategoryId,
            input.Name,
            input.Image,
            input.Price,
            input.HasPay,
            input.Introduce,
            input.StartAt,
            input.EndAt);

        await _courseRepository.UpdateAsync(course);

        return ObjectMapper.Map<Course, CourseDto>(course);
    }

    public async Task<CourseCategoryDto> CreateCategoryAsync(CourseCategoryInput input)
    {
        var courseCategory = ObjectMapper.Map<CourseCategoryInput, CourseCategory>(input);
        await _courseCategoryRepository.InsertAsync(courseCategory);

        return ObjectMapper.Map<CourseCategory, CourseCategoryDto>(courseCategory);
    }

    public async Task UpdateCourseItemAsync(CourseItemInput input)
    {
        // 聚合根 => 课程目录在课程聚合根上
        var course = await _courseRepository.GetAsync(input.CourseId);
        // 更新聚合根子对象
        course.SetCourseItem(GuidGenerator,
            input.Title,
            input.Order,
            input.Duration,
            input.Video,
            input.StartAt,
            input.EndAt);

        // 聚合根直接更新，EFCore完成子对象保存
        await _courseRepository.UpdateAsync(course);
    }

    public async Task CreateCourseTeacherAsync(Guid id, Guid teacherId)
    {
        var course = await _courseRepository.GetAsync(id);
        var notData = course.AddCourseTeacher(GuidGenerator, teacherId);
        if (notData)
        {
            await _courseRepository.InsertAsync(course);
        }
    }

    [Authorize(EnterprisePermissions.Courses.Delete)]
    public async Task<ApiResponse> RemoveAsync(Guid id)
    {
        var response = new ApiResponse<ApiResponse>();

        var has = await _courseManager.HasCourseTeacherForCourse(id);
        if (has)
        {
            response.IsFailed("包含教师，不可删除");

            return response;
        }

        await _teacherRepository.DeleteAsync(id);

        return response;
    }

    public async Task<CourseDto> GetAsync(Guid id)
    {
        var course = await _courseRepository.GetAsync(id, includeDetails: true);
        var courseDto = ObjectMapper.Map<Course, CourseDto>(course);
        if (course.CourseItems.Any())
        {
            courseDto.CourseItems = ObjectMapper.Map<List<CourseItem>, List<CourseItemDto>>(course.CourseItems);
        }

        if (course.CourseTeachers.Any())
        {
            var teacherIds = course.CourseTeachers.Select(ct => ct.TeacherId).ToArray();
            var teacherList = await _teacherRepository.GetByIds(teacherIds);
            courseDto.Teachers = ObjectMapper.Map<List<Teacher>, List<TeacherDto>>(teacherList);
        }

        return courseDto;
    }

    public async Task<ListResultDto<CourseCategoryDto>> GetCategoryAsync()
    {
        var categoryList = await _courseCategoryRepository.GetListAsync();
        var categoryDtoList = ObjectMapper.Map<List<CourseCategory>, List<CourseCategoryDto>>(categoryList);

        return new ListResultDto<CourseCategoryDto>(categoryDtoList);
    }

    public async Task<ListResultDto<CourseDto>> GetListAsync(Guid mId)
    {
        var courseList = await _courseRepository.GetListAsync(c => c.MechanismId == mId, true);
        var courseDtoList = ObjectMapper.Map<List<Course>, List<CourseDto>>(courseList);

        return new ListResultDto<CourseDto>(courseDtoList);
    }

    public async Task<ListResultDto<CourseDto>> GetListAsync()
    {
        var courseList = await _courseRepository.GetListAsync(true);
        var courseDtoList = ObjectMapper.Map<List<Course>, List<CourseDto>>(courseList);

        return new ListResultDto<CourseDto>(courseDtoList); ;
    }

    public async Task<PagedResultDto<CourseDto>> GetPagedListAsync(GetCoursesInput input)
    {
        var hasMultiTenant = CurrentTenant.Id.HasValue;
        using (var disposable = hasMultiTenant ?
            _dataFilter.Enable<IMultiTenant>() : _dataFilter.Disable<IMultiTenant>())
        {
            var count = await _courseRepository.GetCountAsync();
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = "id desc";
            }

            var list = await _courseRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);

            return new PagedResultDto<CourseDto>(
                count,
                ObjectMapper.Map<List<Course>, List<CourseDto>>(list)
            );
        }
    }
}
