using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace MicroClassroom.Enterprise;

public class BannerManager : DomainService
{
    private readonly IBannerRepository _bannerRepository;
    private readonly IMechanismRepository _mechanismRepository;

    public BannerManager(IBannerRepository bannerRepository,
        IMechanismRepository mechanismRepository)
    {
        _bannerRepository = bannerRepository;
        _mechanismRepository = mechanismRepository;
    }

    public async Task<Banner> CreateAsync(string title,
    string image,
    Guid? tenantId)
    {
        Check.NotNull(title, nameof(title));
        Check.NotNull(image, nameof(image));

        await ValidateNameAsync(title);

        var mechanism = await _mechanismRepository.GetSingleAsync();

        return new Banner(GuidGenerator.Create(), mechanism.Id, title, image, tenantId);
    }

    public async Task<Banner> UpdateAsync(Guid id,
        string title,
        string image)
    {
        Check.NotNull(title, nameof(title));
        Check.NotNull(image, nameof(image));

        var banner = await _bannerRepository.GetAsync(id);

        if (banner.Title != title)
        {
            await ValidateNameAsync(title);
        }

        banner.SetValue(title, image);

        return banner;
    }


    private async Task ValidateNameAsync(string title, Guid? expectedId = null)
    {
        var banner = await _bannerRepository.FindAsync(m => m.Title == title);
        if (banner != null && banner.Id != expectedId)
        {
            throw new UserFriendlyException("Duplicate Banner title: " + title);
            //TODO: A domain exception would be better..?
        }
    }
}
