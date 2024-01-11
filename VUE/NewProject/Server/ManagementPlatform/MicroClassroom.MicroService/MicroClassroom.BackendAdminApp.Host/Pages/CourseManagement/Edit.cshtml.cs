using MicroClassroom.Enterprise;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.BlobStoring;
using Volo.Abp.ObjectExtending;

namespace MicroClassroom.BackendAdminApp.Host.Pages.CourseManagement;

public class EditModel : BackendAdminAppPageModel
{
    [BindProperty]
    public CourseViewModel Course { get; set; }
    public List<SelectListItem> CategoryList { get; private set; } = new List<SelectListItem>();
    private readonly ICourseAppService _courseAppService;
    private readonly IBlobContainer<CourseBlobContainer> _blobContainer;
    private readonly ILogger<EditModel> _logger;

    public EditModel(ICourseAppService courseAppService,
        IBlobContainer<CourseBlobContainer> blobContainer)
    {
        _courseAppService = courseAppService;
        _blobContainer = blobContainer;
        _logger = NullLogger<EditModel>.Instance;
    }

    public virtual async Task<IActionResult> OnGetAsync(Guid id)
    {
        var course = await _courseAppService.GetAsync(id);
        Course = ObjectMapper.Map<CourseDto, CourseViewModel>(course);

        var categoryDtoList = await _courseAppService.GetCategoryAsync();
        var selectItems = categoryDtoList.Items
                       .Select(item =>
                        new SelectListItem
                        {
                            Value = item.Id.ToString(),
                            Text = item.Name,
                            Selected = item.Id == Course.CategoryId
                        })
                       .ToList();
        selectItems.Insert(0, new SelectListItem { Value = "", Text = "--«Î—°‘Ò--" });
        CategoryList = selectItems;

        return Page();
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (Course.File != null)
            {
                var imageName = await UploadImage();
                Course.Image = imageName;
            }

            ValidateModel();

            var input = ObjectMapper.Map<CourseViewModel, UpdateCourseInput>(Course);
            await _courseAppService.UpdateCourseAsync(Course.Id, input);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.StackTrace + ex.Message);
        }

        return NoContent();
    }

    private async Task<string> UploadImage()
    {
        var fileInfo = new FileInfo(Course.File.FileName);
        string imageName = string.Format("{0}{1}", GuidGenerator.Create(), fileInfo.Extension);
        using var memoryStream = new MemoryStream();
        await Course.File.CopyToAsync(memoryStream);
        await _blobContainer.SaveAsync(imageName, memoryStream.ToArray(), overrideExisting: true);

        return imageName;
    }

    public class CourseViewModel : ExtensibleObject
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Guid CategoryId { get; set; }

        public decimal Price { get; set; }

        public bool HasPay { get; set; }

        public IFormFile File { get; set; }

        [Required]
        [HiddenInput]
        public string Image { get; set; }

        public string Introduce { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? StartAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EndAt { get; set; }
    }
}
