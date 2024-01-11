using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace MicroClassroom.Enterprise;

public class CourseManager : DomainService
{
    private readonly IRepository<CourseCategory> _courseCategoryRepository;
    private readonly IRepository<CourseTeacher> _courseTeacherRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IMechanismRepository _mechanismRepository;

    public CourseManager(IRepository<CourseCategory> courseCategoryRepository,
        IRepository<CourseTeacher> courseTeacherRepository,
        ICourseRepository courseRepository,
        IMechanismRepository mechanismRepository)
    {
        _courseCategoryRepository = courseCategoryRepository;
        _courseTeacherRepository = courseTeacherRepository;
        _courseRepository = courseRepository;
        _mechanismRepository = mechanismRepository;
    }

    // 领域服务构建
    public async Task<Course> CreateAsync(Guid categoryId,
        string name,
        string image,
        decimal price,
        bool? hasPay,
        string introduce,
        DateTime? startAt,
        DateTime? endAt,
        Guid? tenantId)
    {
        Check.NotNull(categoryId, nameof(categoryId));
        Check.NotNull(name, nameof(name));
        Check.NotNull(image, nameof(image));

        await ValidateNameAsync(name);

        // 机构id
        var mechanism = await _mechanismRepository.GetSingleAsync();

        // 构建一个聚合根对象 ，并不是数据保存
        return new Course(GuidGenerator.Create(), mechanism.Id, categoryId, name, image, price, hasPay, introduce, startAt, endAt, tenantId);
    }

    public async Task<Course> CreateAsync(Guid mechanismId,
        Guid categoryId,
        string name,
        string image,
        decimal price,
        bool? hasPay,
        string introduce,
        DateTime? startAt,
        DateTime? endAt,
        Guid? tenantId)
    {
        Check.NotNull(mechanismId, nameof(mechanismId));
        Check.NotNull(categoryId, nameof(categoryId));
        Check.NotNull(name, nameof(name));
        Check.NotNull(image, nameof(image));

        await ValidateNameAsync(name);

        return new Course(GuidGenerator.Create(), mechanismId, categoryId, name, image, price, hasPay, introduce, startAt, endAt, tenantId);
    }

    public async Task<Course> UpdateAsync(Guid id,
        Guid categoryId,
        string name,
        string image,
        decimal price,
        bool? hasPay,
        string introduce,
        DateTime? startAt,
        DateTime? endAt)
    {
        Check.NotNull(categoryId, nameof(categoryId));
        Check.NotNull(name, nameof(name));
        Check.NotNull(image, nameof(image));

        var course = await _courseRepository.GetAsync(id);

        if (course.Name != name)
        {
            await ValidateNameAsync(name);
        }

        course.SetCourse(categoryId, name, image, price, hasPay, introduce, startAt, endAt);

        return course;
    }

    public async Task<CourseCategory> CreateCategoryAsync(string name, int? status, Guid? tenantId)
    {
        Check.NotNull(name, nameof(name));

        await ValidateCategoryNameAsync(name);

        return new CourseCategory(GuidGenerator.Create(), name, status, tenantId);
    }

    public async Task<bool> HasCourseTeacherForTeacher(Guid teacherId)
    {
        return await _courseTeacherRepository.AnyAsync(ct => ct.TeacherId == teacherId);
    }

    public async Task<bool> HasCourseTeacherForCourse(Guid courseId)
    {
        return await _courseTeacherRepository.AnyAsync(ct => ct.CourseId == courseId);
    }

    public async Task<bool> HasMechanismCourse(Guid id)
    {
        return await _courseRepository.AnyAsync(c => c.MechanismId == id);
    }

    private async Task ValidateNameAsync(string name, Guid? expectedId = null)
    {
        var course = await _courseRepository.FindAsync(m => m.Name == name);
        if (course != null && course.Id != expectedId)
        {
            throw new UserFriendlyException("Duplicate Course name: " + name);
            //TODO: A domain exception would be better..?
        }
    }

    private async Task ValidateCategoryNameAsync(string name, Guid? expectedId = null)
    {
        var courseCategory = await _courseCategoryRepository.FindAsync(m => m.Name == name);
        if (courseCategory != null && courseCategory.Id != expectedId)
        {
            throw new UserFriendlyException("Duplicate Course Category name: " + name);
            //TODO: A domain exception would be better..?
        }
    }
}
