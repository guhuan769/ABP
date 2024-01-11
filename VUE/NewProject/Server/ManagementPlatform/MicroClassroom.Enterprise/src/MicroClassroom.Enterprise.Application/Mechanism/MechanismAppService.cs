using MicroClassroom.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;

namespace MicroClassroom.Enterprise;

public class MechanismAppService : EnterpriseAppService, IMechanismAppService
{
    private readonly IMechanismRepository _mechanismRepository;
    private readonly MechanismManager _mechanismManager;
    private readonly CourseManager _courseManager;
    private readonly ITenantRepository _tenantRepository;
    private readonly ITenantManager _tenantManager;
    private readonly IDataFilter _dataFilter;
    private readonly IDataSeeder _dataSeeder;

    public MechanismAppService(IMechanismRepository mechanismRepository,
        MechanismManager mechanismManager,
        ITenantRepository tenantRepository,
        ITenantManager tenantManager,
        CourseManager courseManager,
        IDataFilter dataFilter,
        IDataSeeder dataSeeder)
    {
        _mechanismRepository = mechanismRepository;
        _mechanismManager = mechanismManager;
        _tenantRepository = tenantRepository;
        _tenantManager = tenantManager;
        _courseManager = courseManager;
        _dataFilter = dataFilter;
        _dataSeeder = dataSeeder;
    }

    public async Task<MechanismDto> CreateAsync(CreateMechanismInput input)
    {
        // 创建机构同时创建租户
        var mechanism = await _mechanismManager.CreateAsync(input.Name,
                   input.Pinyin,
                   input.Image,
                   input.Slogo,
                   input.Introduce,
                   input.About,
                   CurrentTenant.Id);

        await _mechanismRepository.InsertAsync(mechanism);

        var tenant = await _tenantManager.CreateAsync(input.Pinyin);
        if (!input.ConnectionString.IsNullOrWhiteSpace())
        {
            tenant.SetConnectionString("Default", input.ConnectionString);
        }
        await _tenantRepository.InsertAsync(tenant);

        await CurrentUnitOfWork.SaveChangesAsync();

        // 初始化多租户数据
        using (CurrentTenant.Change(tenant.Id, tenant.Name))
        {
            //TODO: Handle database creation?
            // TODO: Seeder might be triggered via event handler.
            await _dataSeeder.SeedAsync(new DataSeedContext(tenant.Id)
            .WithProperty(IdentityDataSeedContributor.AdminEmailPropertyName, IdentityDataSeedContributor.AdminEmailDefaultValue)
            .WithProperty(IdentityDataSeedContributor.AdminPasswordPropertyName, IdentityDataSeedContributor.AdminPasswordDefaultValue));
        }

        return ObjectMapper.Map<Mechanism, MechanismDto>(mechanism);
    }

    public async Task<MechanismDto> UpdateAsync(Guid id, UpdateMechanismInput input)
    {
        var hasMultiTenant = CurrentTenant.Id.HasValue;
        using (var disposable = hasMultiTenant ?
            _dataFilter.Enable<IMultiTenant>() : _dataFilter.Disable<IMultiTenant>())
        {
            var mechanism = await _mechanismManager.UpdateAsync(id,
                input.Name,
                input.Pinyin,
                input.Image,
                input.Slogo,
                input.Introduce,
                input.About);

            await _mechanismRepository.UpdateAsync(mechanism);

            return ObjectMapper.Map<Mechanism, MechanismDto>(mechanism);
        }
    }

    public async Task<ApiResponse> RemoveAsync(Guid id)
    {
        var response = new ApiResponse<ApiResponse>();

        var has = await _courseManager.HasMechanismCourse(id);
        if (has)
        {
            response.IsFailed("包含课程，不可删除");

            return response;
        }

        await _mechanismRepository.DeleteAsync(id);

        return response;
    }

    public async Task<MechanismDto> GetAsync(Guid id)
    {
        var hasMultiTenant = CurrentTenant.Id.HasValue;
        using (var disposable = hasMultiTenant ?
            _dataFilter.Enable<IMultiTenant>() : _dataFilter.Disable<IMultiTenant>())
        {
            var mechanism = await _mechanismRepository.GetAsync(id, includeDetails: true);
            var mechanismDto = ObjectMapper.Map<Mechanism, MechanismDto>(mechanism);
            if (mechanism.Teachers != null)
            {
                mechanismDto.Teachers = ObjectMapper.Map<List<Teacher>, List<TeacherDto>>(mechanism.Teachers);
            }

            if (mechanism.Courses != null)
            {
                mechanismDto.Courses = ObjectMapper.Map<List<Course>, List<CourseDto>>(mechanism.Courses);
            }

            return mechanismDto;
        }
    }

    public async Task<MechanismDto> GetTenantAsync(Guid tenantId)
    {
        using (var disposable = CurrentTenant.IsAvailable ?
            _dataFilter.Enable<IMultiTenant>() : _dataFilter.Disable<IMultiTenant>())
        {
            var mechanism = await _mechanismRepository.GetTenantAsync(tenantId);
            var mechanismDto = ObjectMapper.Map<Mechanism, MechanismDto>(mechanism);
            if (mechanism.Teachers != null)
            {
                mechanismDto.Teachers = ObjectMapper.Map<List<Teacher>, List<TeacherDto>>(mechanism.Teachers);
            }

            if (mechanism.Courses != null)
            {
                mechanismDto.Courses = ObjectMapper.Map<List<Course>, List<CourseDto>>(mechanism.Courses);
            }

            return mechanismDto;
        }
    }

    public async Task<PagedResultDto<MechanismDto>> GetPagedListAsync(GetMechanismsInput input)
    {
        var hasMultiTenant = CurrentTenant.Id.HasValue;
        using (var disposable = hasMultiTenant ?
            _dataFilter.Enable<IMultiTenant>() : _dataFilter.Disable<IMultiTenant>())
        {
            var count = await _mechanismRepository.GetCountAsync();
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = "id desc";
            }

            var list = await _mechanismRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);

            return new PagedResultDto<MechanismDto>(
                count,
                ObjectMapper.Map<List<Mechanism>, List<MechanismDto>>(list)
            );
        }
    }

}
