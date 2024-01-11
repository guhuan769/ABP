using MicroClassroom.Enterprise;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.BlobStoring;
using Volo.Abp.ObjectExtending;

namespace MicroClassroom.BackendAdminApp.Host.Pages.CourseManagement;

public class NewModel : BackendAdminAppPageModel
{
    [BindProperty]
    public CourseViewModel Course { get; set; }

    public List<SelectListItem> CategoryList { get; private set; } = new List<SelectListItem>();

    private readonly ICourseAppService _courseAppService;
    private readonly ILogger<NewModel> _logger;
    private readonly IBlobContainer<CourseBlobContainer> _blobContainer;

    public NewModel(ICourseAppService courseAppService,
        IBlobContainer<CourseBlobContainer> blobContainer)
    {
        _courseAppService = courseAppService;
        _blobContainer = blobContainer;
        _logger = NullLogger<NewModel>.Instance;
    }

    public virtual async Task<IActionResult> OnGetAsync()
    {
        Course = new CourseViewModel();

        var categoryDtoList = await _courseAppService.GetCategoryAsync();
        var selectItems = categoryDtoList.Items
                       .Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name })
                       .ToList();
        selectItems.Insert(0, new SelectListItem { Value = "", Text = "--«Î—°‘Ò--" });
        CategoryList = selectItems;

        return await Task.FromResult<IActionResult>(Page());
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        ValidateModel();

        try
        {
            var imageName = await UploadImage();

            var input = ObjectMapper.Map<CourseViewModel, CreateCourseInput>(Course);
            input.Image = imageName;
            await _courseAppService.CreateCourseAsync(input);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.StackTrace + ex.Message);
        }

        return NoContent();
    }

    private async Task<string> UploadImage()
    {
        var fileInfo = new FileInfo(Course.Image.FileName);
        string imageName = string.Format("{0}{1}", GuidGenerator.Create(), fileInfo.Extension);
        using var memoryStream = new MemoryStream();
        await Course.Image.CopyToAsync(memoryStream);
        await _blobContainer.SaveAsync(imageName, memoryStream.ToArray(), overrideExisting: true);

        return imageName;
    }

    public class CourseViewModel : ExtensibleObject
    {
        [Required]
        public string Name { get; set; }

        public Guid CategoryId { get; set; }

        public decimal Price { get; set; }

        public bool HasPay { get; set; }

        public IFormFile Image { get; set; }

        public string Introduce { get; set; }

        public DateTime? StartAt { get; set; }

        public DateTime? EndAt { get; set; }
    }
}
