using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class SubgridModel : PageModel
    {
        private readonly ILogger<SubgridModel> _logger;

        public SubgridModel(ILogger<SubgridModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }

}
