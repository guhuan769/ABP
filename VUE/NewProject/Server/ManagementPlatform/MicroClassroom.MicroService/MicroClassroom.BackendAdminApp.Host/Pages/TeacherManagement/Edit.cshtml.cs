using MicroClassroom.Enterprise;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.BlobStoring;
using Volo.Abp.ObjectExtending;

namespace MicroClassroom.BackendAdminApp.Host.Pages.TeacherManagement;

public class EditModel : BackendAdminAppPageModel
{
    [BindProperty]
    public TeacherViewModel Teacher { get; set; }

    private readonly ITeacherAppService _teacherAppService;
    private readonly IBlobContainer<TeacherBlobContainer> _blobContainer;
    private readonly ILogger<EditModel> _logger;

    public EditModel(ITeacherAppService teacherAppService,
        IBlobContainer<TeacherBlobContainer> blobContainer)
    {
        _teacherAppService = teacherAppService;
        _blobContainer = blobContainer;
        _logger = NullLogger<EditModel>.Instance;
    }

    public virtual async Task<IActionResult> OnGetAsync(Guid id)
    {
        var teacher = await _teacherAppService.GetAsync(id);
        Teacher = ObjectMapper.Map<TeacherDto, TeacherViewModel>(teacher);

        return Page();
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var imageName = await UploadImage();
            Teacher.Image = imageName;
            ValidateModel();

            var input = ObjectMapper.Map<TeacherViewModel, UpdateTeacherInput>(Teacher);
            await _teacherAppService.UpdateAsync(Teacher.Id, input);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.StackTrace + ex.Message);
        }

        return NoContent();
    }

    private async Task<string> UploadImage()
    {
        var fileInfo = new FileInfo(Teacher.File.FileName);
        string imageName = string.Format("{0}{1}", GuidGenerator.Create(), fileInfo.Extension);
        using var memoryStream = new MemoryStream();
        await Teacher.File.CopyToAsync(memoryStream);
        await _blobContainer.SaveAsync(imageName, memoryStream.ToArray(), overrideExisting: true);

        return imageName;
    }

    public class TeacherViewModel : ExtensibleObject
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IFormFile File { get; set; }

        [HiddenInput]
        public string Image { get; set; }

        public string Introduce { get; set; }
    }
}
