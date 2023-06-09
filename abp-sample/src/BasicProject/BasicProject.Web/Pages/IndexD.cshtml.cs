using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BasicProject.Web.Pages
{
    public class IndexDModel : PageModel
    {
        private readonly ILogger<IndexDModel> _logger;

        public IndexDModel(ILogger<IndexDModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}