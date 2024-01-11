using MicroClassroom.Enterprise;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.BlobStoring;
using Volo.Abp.ObjectExtending;

namespace MicroClassroom.BackendAdminApp.Host.Pages.BannerManagement;

public class NewModel : BackendAdminAppPageModel
{
    [BindProperty]
    public BannerViewModel Banner { get; set; }

    private readonly IBannerAppService _bannerAppService;
    private readonly IBlobContainer<BannerBlobContainer> _blobContainer;
    private readonly ILogger<NewModel> _logger;

    public NewModel(IBannerAppService bannerAppService,
        IBlobContainer<BannerBlobContainer> blobContainer)
    {
        _bannerAppService = bannerAppService;
        _blobContainer = blobContainer;
        _logger = NullLogger<NewModel>.Instance;
    }

    public virtual async Task<IActionResult> OnGetAsync()
    {
        Banner = new BannerViewModel();

        return await Task.FromResult<IActionResult>(Page());
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        ValidateModel();

        try
        {
            var imageName = await UploadImage();

            var input = ObjectMapper.Map<BannerViewModel, CreateBannerInput>(Banner);
            input.Image = imageName;
            await _bannerAppService.CreateAsync(input);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.StackTrace + ex.Message);
        }

        return NoContent();
    }

    private async Task<string> UploadImage()
    {
        var fileInfo = new FileInfo(Banner.Image.FileName);
        string imageName = string.Format("{0}{1}", GuidGenerator.Create(), fileInfo.Extension);
        using var memoryStream = new MemoryStream();
        await Banner.Image.CopyToAsync(memoryStream);
        await _blobContainer.SaveAsync(imageName, memoryStream.ToArray(), overrideExisting: true);

        return imageName;
    }

    public class BannerViewModel : ExtensibleObject
    {
        [Required]
        public string Title { get; set; }

        public IFormFile Image { get; set; }
    }
}
