using MicroClassroom.Enterprise.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace MicroClassroom.Enterprise;

[Area(EnterpriseRemoteServiceConsts.ModuleName)]
[RemoteService(Name = EnterpriseRemoteServiceConsts.RemoteServiceName)]
[Route("api/enterprise/banner")]
public class BannerController : EnterpriseController, IBannerAppService
{
    private readonly IBannerAppService _bannerAppService;

    public BannerController(IBannerAppService bannerAppService)
    {
        _bannerAppService = bannerAppService;
    }

    [HttpPost("create")]
    public async Task<BannerDto> CreateAsync(CreateBannerInput input)
    {
        return await _bannerAppService.CreateAsync(input);
    }

    [HttpPost("update/{id}")]
    public async Task<BannerDto> UpdateAsync(Guid id, UpdateBannerInput input)
    {
        return await _bannerAppService.UpdateAsync(id, input);
    }

    [HttpGet("{id}")]
    public async Task<BannerDto> GetAsync(Guid id)
    {
        return await _bannerAppService.GetAsync(id);
    }

    [HttpGet("page-list")]
    public async Task<PagedResultDto<BannerDto>> GetPagedListAsync(GetBannerInput input)
    {
        return await _bannerAppService.GetPagedListAsync(input);
    }

    [HttpDelete("remove")]
    [Authorize(EnterprisePermissions.Banners.Delete)]
    public async Task RemoveAsync(Guid id)
    {
        await _bannerAppService.RemoveAsync(id);
    }
}
