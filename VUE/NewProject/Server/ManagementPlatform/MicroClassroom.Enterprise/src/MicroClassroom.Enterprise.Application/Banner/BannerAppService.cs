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

public class BannerAppService : EnterpriseAppService, IBannerAppService
{
    private readonly IBannerRepository _bannerRepository;
    private readonly BannerManager _bannerManager;
    private readonly IDataFilter _dataFilter;

    public BannerAppService(IBannerRepository bannerRepository,
        BannerManager bannerManager,
        IDataFilter dataFilter)
    {
        _bannerRepository = bannerRepository;
        _bannerManager = bannerManager;
        _dataFilter = dataFilter;
    }

    public async Task<BannerDto> CreateAsync(CreateBannerInput input)
    {
        var banner = await _bannerManager.CreateAsync(input.Title,
            input.Image,
            CurrentTenant.Id);

        await _bannerRepository.InsertAsync(banner);

        return ObjectMapper.Map<Banner, BannerDto>(banner);
    }

    public async Task<BannerDto> UpdateAsync(Guid id, UpdateBannerInput input)
    {
        var banner = await _bannerManager.UpdateAsync(id,
            input.Title,
            input.Image);

        await _bannerRepository.UpdateAsync(banner);

        return ObjectMapper.Map<Banner, BannerDto>(banner);
    }

    [Authorize(EnterprisePermissions.Banners.Delete)]
    public async Task RemoveAsync(Guid id)
    {
        await _bannerRepository.DeleteAsync(id);
    }

    public async Task<BannerDto> GetAsync(Guid id)
    {
        var banner = await _bannerRepository.GetAsync(id);

        return ObjectMapper.Map<Banner, BannerDto>(banner);
    }

    public async Task<PagedResultDto<BannerDto>> GetPagedListAsync(GetBannerInput input)
    {
        var hasMultiTenant = CurrentTenant.Id.HasValue;
        using (var disposable = hasMultiTenant ?
            _dataFilter.Enable<IMultiTenant>() : _dataFilter.Disable<IMultiTenant>())
        {
            var count = await _bannerRepository.GetCountAsync();
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = "id desc";
            }

            var list = await _bannerRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);

            return new PagedResultDto<BannerDto>(
                count,
                ObjectMapper.Map<List<Banner>, List<BannerDto>>(list)
            );
        }
    }
}
