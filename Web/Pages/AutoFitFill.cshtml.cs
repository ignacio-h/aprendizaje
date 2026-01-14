using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class AutoFitFillModel : PageModel
    {
        private readonly ILogger<AutoFitFillModel> _logger;

        public AutoFitFillModel(ILogger<AutoFitFillModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
