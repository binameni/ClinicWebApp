using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicWebApp.Pages.Shared
{
    public class PageModelWrapper : PageModel
    {
        [TempData]
        public string? Message { get; set; }

        [TempData]
        public bool IsError { get; set; }

        public void SetPrompt(string message, bool isError = false)
        {
            IsError = isError;
            Message = message;
        }
    }
}
