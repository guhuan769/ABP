using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MicroClassroom.Enterprise;

public interface IBannerAppService : IApplicationService
{
    Task<BannerDto> CreateAsync(CreateBannerInput input);

    Task<BannerDto> UpdateAsync(Guid id, UpdateBannerInput input);

    Task RemoveAsync(Guid id);

    Task<BannerDto> GetAsync(Guid id);

    Task<PagedResultDto<BannerDto>> GetPagedListAsync(GetBannerInput input);
}
