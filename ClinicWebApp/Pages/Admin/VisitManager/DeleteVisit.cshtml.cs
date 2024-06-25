using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using ClinicWebApp.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicWebApp.Pages.Admin.VisitManager
{
    [Authorize(Roles = nameof(ApplicationRoles.Doctor))]
    public class DeleteVisitModel(IVisitRepository visitRepository) : PageModelWrapper
    {
        private readonly IVisitRepository _visitRepository = visitRepository;

        [FromQuery]
        public int Id { get; set; }

        [FromQuery]
        public string SelectedDate { get; set; }

        public async Task<IActionResult> OnGet()
        {
            await _visitRepository.RemoveAndSave(Id);
            this.SetPrompt("نوبت حذف شد");
            return RedirectToPage("VisitListManager", routeValues: new { SelectedDate = SelectedDate });
        }

        public async Task<IActionResult> OnGetAll()
        {
            await _visitRepository.Clear(SelectedDate);
            this.SetPrompt("نوبت ها حذف شدند");
            return RedirectToPage("VisitListManager", routeValues: new { SelectedDate = SelectedDate });
        }
    }
}
