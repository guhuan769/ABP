using MicroClassroom.Enterprise;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.BlobStoring;
using Volo.Abp.ObjectExtending;

namespace MicroClassroom.BackendAdminApp.Host.Pages.MechanismManagement;

public class InfoModel : BackendAdminAppPageModel
{
    [BindProperty]
    public MechanismViewModel Mechanism { get; set; }

    private readonly IMechanismAppService _mechanismAppService;
    private readonly IBlobContainer<MechanismBlobContainer> _blobContainer;
    private readonly ILogger<InfoModel> _logger;

    public InfoModel(IMechanismAppService mechanismAppService,
        IBlobContainer<MechanismBlobContainer> blobContainer)
    {
        _mechanismAppService = mechanismAppService;
        _blobContainer = blobContainer;
        _logger = NullLogger<InfoModel>.Instance;
    }

    public virtual async Task<IActionResult> OnGetAsync(Guid id)
    {
        var mechanism = await _mechanismAppService.GetTenantAsync(id);
        Mechanism = ObjectMapper.Map<MechanismDto, MechanismViewModel>(mechanism);

        return Page();
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (Mechanism.File != null)
            {
                var imageName = await UploadImage();
                Mechanism.Image = imageName;
            }

            ValidateModel();

            var input = ObjectMapper.Map<MechanismViewModel, UpdateMechanismInput>(Mechanism);
            await _mechanismAppService.UpdateAsync(Mechanism.Id, input);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.StackTrace + ex.Message);
        }

        return NoContent();
    }

    private async Task<string> UploadImage()
    {
        var fileInfo = new FileInfo(Mechanism.File.FileName);
        string imageName = string.Format("{0}{1}", GuidGenerator.Create(), fileInfo.Extension);
        using var memoryStream = new MemoryStream();
        await Mechanism.File.CopyToAsync(memoryStream);
        await _blobContainer.SaveAsync(imageName, memoryStream.ToArray(), overrideExisting: true);

        return imageName;
    }

    public class MechanismViewModel : ExtensibleObject
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Pinyin { get; set; }

        public IFormFile File { get; set; }

        [Required]
        [HiddenInput]
        public string Image { get; set; }

        [Required]
        public string Slogo { get; set; }

        [Required]
        public string Introduce { get; set; }

        public int? Grade { get; set; }

        [Required]
        public string About { get; set; }
    }
}
