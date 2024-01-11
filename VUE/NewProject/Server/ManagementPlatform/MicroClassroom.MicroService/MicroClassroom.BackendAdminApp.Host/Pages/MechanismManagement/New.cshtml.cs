using MicroClassroom.Enterprise;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.BlobStoring;
using Volo.Abp.ObjectExtending;

namespace MicroClassroom.BackendAdminApp.Host.Pages.MechanismManagement;

public class NewModel : BackendAdminAppPageModel
{
    [BindProperty]
    public MechanismViewModel Mechanism { get; set; }

    private readonly IMechanismAppService _mechanismAppService;
    private readonly IBlobContainer<MechanismBlobContainer> _blobContainer;
    private readonly ILogger<NewModel> _logger;

    public NewModel(IMechanismAppService mechanismAppService,
        IBlobContainer<MechanismBlobContainer> blobContainer)
    {
        _mechanismAppService = mechanismAppService;
        _blobContainer = blobContainer;
        _logger = NullLogger<NewModel>.Instance;
    }

    public virtual async Task<IActionResult> OnGetAsync()
    {
        Mechanism = new MechanismViewModel();

        return await Task.FromResult<IActionResult>(Page());
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        ValidateModel();

        try
        {
            var imageName = await UploadImage();

            var input = ObjectMapper.Map<MechanismViewModel, CreateMechanismInput>(Mechanism);
            input.Image = imageName;
            await _mechanismAppService.CreateAsync(input);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.StackTrace + ex.Message);
        }

        return NoContent();
    }

    private async Task<string> UploadImage()
    {
        var fileInfo = new FileInfo(Mechanism.Image.FileName);
        string imageName = string.Format("{0}{1}", GuidGenerator.Create(), fileInfo.Extension);
        using var memoryStream = new MemoryStream();
        await Mechanism.Image.CopyToAsync(memoryStream);
        await _blobContainer.SaveAsync(imageName, memoryStream.ToArray(), overrideExisting: true);

        return imageName;
    }

    public class MechanismViewModel : ExtensibleObject
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Pinyin { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public string Slogo { get; set; }

        [Required]
        public string Introduce { get; set; }

        public string About { get; set; }
    }
}
