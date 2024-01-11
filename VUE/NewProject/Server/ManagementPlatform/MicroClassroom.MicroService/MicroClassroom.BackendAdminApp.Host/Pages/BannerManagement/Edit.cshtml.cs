using MicroClassroom.Enterprise;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.BlobStoring;
using Volo.Abp.ObjectExtending;

namespace MicroClassroom.BackendAdminApp.Host.Pages.BannerManagement;

public class EditModel : BackendAdminAppPageModel
{
    [BindProperty]
    public BannerViewModel Banner { get; set; }

    private readonly IBannerAppService _bannerAppService;
    private readonly IBlobContainer<BannerBlobContainer> _blobContainer;
    private readonly ILogger<NewModel> _logger;

    public EditModel(IBannerAppService bannerAppService,
        IBlobContainer<BannerBlobContainer> blobContainer)
    {
        _bannerAppService = bannerAppService;
        _blobContainer = blobContainer;
        _logger = NullLogger<NewModel>.Instance;
    }

    public virtual async Task<IActionResult> OnGetAsync(Guid id)
    {
        var banner = await _bannerAppService.GetAsync(id);
        Banner = ObjectMapper.Map<BannerDto, BannerViewModel>(banner);

        return Page();
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var imageName = await UploadImage();
            Banner.Image = imageName;
            ValidateModel();

            var input = ObjectMapper.Map<BannerViewModel, UpdateBannerInput>(Banner);
            await _bannerAppService.UpdateAsync(Banner.Id, input);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.StackTrace + ex.Message);
        }

        return NoContent();
    }

    private async Task<string> UploadImage()
    {
        var fileInfo = new FileInfo(Banner.File.FileName);
        string imageName = string.Format("{0}{1}", GuidGenerator.Create(), fileInfo.Extension);
        using var memoryStream = new MemoryStream();
        await Banner.File.CopyToAsync(memoryStream);
        await _blobContainer.SaveAsync(imageName, memoryStream.ToArray(), overrideExisting: true);

        return imageName;
    }

    public class BannerViewModel : ExtensibleObject
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public IFormFile File { get; set; }

        [HiddenInput]
        public string Image { get; set; }
    }
}
