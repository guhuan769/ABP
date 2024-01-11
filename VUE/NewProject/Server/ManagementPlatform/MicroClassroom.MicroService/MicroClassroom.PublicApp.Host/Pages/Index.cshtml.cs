using MicroClassroom.Enterprise;
using Microsoft.AspNetCore.Authentication;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MicroClassroom.PublicApp.Host.Pages
{
    public class IndexModel : AbpPageModel
    {
        private readonly ICourseAppService _courseAppService;
        private readonly ILogger<IndexModel> _logger;

        public IReadOnlyList<CourseDto> CourseList { get; set; } = new List<CourseDto>();

        public IndexModel(ICourseAppService courseAppService, ILogger<IndexModel> logger)
        {
            _courseAppService = courseAppService;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var courseDtoList = await _courseAppService.GetListAsync();
                CourseList = courseDtoList.Items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace + ex.Message);
            }
        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}