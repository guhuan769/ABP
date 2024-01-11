using MicroClassroom.Enterprise;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.BlobStoring;
using Volo.Abp.ObjectExtending;

namespace MicroClassroom.BackendAdminApp.Host.Pages.TeacherManagement;

public class NewModel : BackendAdminAppPageModel
{
    [BindProperty]
    public TeacherViewModel Teacher { get; set; }

    private readonly ITeacherAppService _teacherAppService;
    private readonly IBlobContainer<TeacherBlobContainer> _blobContainer;
    private readonly ILogger<NewModel> _logger;

    public NewModel(ITeacherAppService teacherAppService,
        IBlobContainer<TeacherBlobContainer> blobContainer)
    {
        _teacherAppService = teacherAppService;
        _blobContainer = blobContainer;
        _logger = NullLogger<NewModel>.Instance;
    }

    public virtual async Task<IActionResult> OnGetAsync()
    {
        Teacher = new TeacherViewModel();

        return await Task.FromResult<IActionResult>(Page());
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        ValidateModel();

        try
        {
            var imageName = await UploadImage();

            var input = ObjectMapper.Map<TeacherViewModel, CreateTeacherInput>(Teacher);
            input.Image = imageName;
            await _teacherAppService.CreateAsync(input);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.StackTrace + ex.Message);
        }

        return NoContent();
    }

    private async Task<string> UploadImage()
    {
        var fileInfo = new FileInfo(Teacher.Image.FileName);
        string imageName = string.Format("{0}{1}", GuidGenerator.Create(), fileInfo.Extension);
        using var memoryStream = new MemoryStream();
        await Teacher.Image.CopyToAsync(memoryStream);
        await _blobContainer.SaveAsync(imageName, memoryStream.ToArray(), overrideExisting: true);

        return imageName;
    }

    public class TeacherViewModel : ExtensibleObject
    {
        [Required]
        public string Name { get; set; }

        public IFormFile Image { get; set; }

        public string Introduce { get; set; }
    }
}
