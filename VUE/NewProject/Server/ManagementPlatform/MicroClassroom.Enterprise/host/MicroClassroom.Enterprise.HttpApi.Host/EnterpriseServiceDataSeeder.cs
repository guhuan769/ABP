//using Magicodes.ExporterAndImporter.Core;
//using Microsoft.Extensions.FileProviders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Volo.Abp;
//using Volo.Abp.Data;
//using Volo.Abp.DependencyInjection;
//using Volo.Abp.Domain.Repositories;
//using Volo.Abp.Guids;
//using Volo.Abp.TenantManagement;
//using Volo.Abp.VirtualFileSystem;

//namespace MicroClassroom.Enterprise;

//public class EnterpriseServiceDataSeeder : IDataSeedContributor, ITransientDependency
//{
//    private readonly ITenantRepository _tenantRepository;
//    private readonly IMechanismRepository _mechanismRepository;
//    private readonly IRepository<CourseCategory> _courseCategoryRepository;
//    private readonly ICourseRepository _courseRepository;
//    private readonly MechanismManager _mechanismManager;
//    private readonly CourseManager _courseManager;
//    private readonly IVirtualFileProvider _virtualFileProvider;
//    private readonly IImporter _importer;
//    private IGuidGenerator _guidGenerator;

//    public EnterpriseServiceDataSeeder(ITenantRepository tenantRepository,
//        IMechanismRepository mechanismRepository,
//        IRepository<CourseCategory> courseCategoryRepository,
//        ICourseRepository courseRepository,
//        MechanismManager mechanismManager,
//        CourseManager courseManager,
//        IVirtualFileProvider virtualFileProvider,
//        IImporter importer,
//        IGuidGenerator guidGenerator)
//    {
//        _tenantRepository = tenantRepository;
//        _mechanismRepository = mechanismRepository;
//        _courseCategoryRepository = courseCategoryRepository;
//        _courseRepository = courseRepository;
//        _mechanismManager = mechanismManager;
//        _courseManager = courseManager;
//        _virtualFileProvider = virtualFileProvider;
//        _importer = importer;
//        _guidGenerator = guidGenerator;
//    }

//    public async Task SeedAsync(DataSeedContext context)
//    {
//        var tenantList = await _tenantRepository.GetListAsync();
//        foreach (var tenant in tenantList)
//        {
//            var mechanism = await GetMechanismAsync(tenant);
//            if (mechanism != null)
//            {
//                await _mechanismRepository.InsertAsync(mechanism);
//            }

//            var courseCategoryList = await GetCourseCategoriesAsync(tenant);
//            if (courseCategoryList.Any())
//            {
//                await _courseCategoryRepository.InsertManyAsync(courseCategoryList);
//            }

//            var courseList = await GetCourseListAsync(mechanism.Id, courseCategoryList, tenant);
//            if (courseList.Any())
//            {
//                await _courseRepository.InsertManyAsync(courseList);
//            }
//        }
//    }

//    private async Task<Mechanism> GetMechanismAsync(Tenant tenant)
//    {
//        var fileInfo = GetFileInfo($"/MicroClassroom/Enterprise/Files/{tenant.Name}/mechanism.xlsx");
//        var import = _importer.Import<ImportMechanismDto>(fileInfo.CreateReadStream());
//        if (import.IsCompleted && import.Result != null)
//        {
//            var dto = import.Result.Data.Single();
//            var mechanism = await _mechanismManager.CreateAsync(dto.Name,
//                tenant.Name,
//                dto.Image,
//                dto.Slogo,
//                dto.Introduce,
//                dto.About,
//                tenant.Id);

//            var teacherDtoList = GetTeacherDtoList(tenant);
//            teacherDtoList.ForEach(teacher =>
//            {
//                mechanism.SetTeacher(_guidGenerator,
//                    teacher.Name,
//                    teacher.Image,
//                    teacher.Introduce);
//            });

//            return mechanism;
//        }

//        return null;
//    }

//    private List<ImportTeacherDto> GetTeacherDtoList(Tenant tenant)
//    {
//        var fileInfo = GetFileInfo($"/MicroClassroom/Enterprise/Files/{tenant.Name}/teacher.xlsx");
//        var import = _importer.Import<ImportTeacherDto>(fileInfo.CreateReadStream());
//        var teacherList = new List<ImportTeacherDto>();
//        if (import.IsCompleted && import.Result != null)
//        {
//            teacherList.AddRange(import.Result.Data);
//        }

//        return teacherList;
//    }

//    private async Task<List<CourseCategory>> GetCourseCategoriesAsync(Tenant tenant)
//    {
//        var fileInfo = GetFileInfo($"/MicroClassroom/Enterprise/Files/{tenant.Name}/category.xlsx");
//        var import = _importer.Import<ImportCourseCategoryDto>(fileInfo.CreateReadStream());
//        var courseCategoryList = new List<CourseCategory>();
//        if (import.IsCompleted && import.Result != null)
//        {
//            var dtoList = import.Result.Data;
//            foreach (var dto in dtoList)
//            {
//                var category = await _courseManager.CreateCategoryAsync(dto.Name, dto.Status, tenant.Id);
//                courseCategoryList.Add(category);
//            }
//        }

//        return courseCategoryList;
//    }

//    private async Task<List<Course>> GetCourseListAsync(Guid mechanismId,
//        List<CourseCategory> courseCategoryList,
//        Tenant tenant)
//    {
//        var fileInfo = GetFileInfo($"/MicroClassroom/Enterprise/Files/{tenant.Name}/course.xlsx");
//        var import = _importer.Import<ImportCourseDto>(fileInfo.CreateReadStream());
//        var courseList = new List<Course>();
//        if (import.IsCompleted && import.Result != null)
//        {
//            var dtoList = import.Result.Data;
//            foreach (var dto in dtoList)
//            {
//                var category = courseCategoryList[new Random().Next(courseCategoryList.Count)];
//                var course = await _courseManager.CreateAsync(mechanismId,
//                    category.Id,
//                    dto.Name,
//                    dto.Image,
//                    dto.Price,
//                    dto.HasPay,
//                    dto.Introduce,
//                    dto.StartAt,
//                    dto.EndAt,
//                    tenant.Id);
//                courseList.Add(course);
//            }
//        }

//        return courseList;
//    }

//    private IFileInfo GetFileInfo(string filePath)
//    {
//        var fileInfo = _virtualFileProvider.GetFileInfo(filePath);
//        if (fileInfo == null)
//        {
//            throw new UserFriendlyException($"`{filePath}` not found file");
//        }

//        return fileInfo;
//    }
//}
