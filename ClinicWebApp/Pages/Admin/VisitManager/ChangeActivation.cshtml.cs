using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using ClinicWebApp.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicWebApp.Pages.Admin.VisitManager
{
    [Authorize(Roles = nameof(ApplicationRoles.Doctor))]
    public class ChangeActivationModel(IVisitRepository visitRepository) : PageModelWrapper
    {
        private readonly IVisitRepository _visitRepository = visitRepository;

        [FromQuery]
        public int Id { get; set; }

        [FromQuery]
        public string SelectedDate { get; set; }

        public async Task<IActionResult> OnGetActivate()
        {
            await _visitRepository.ActivateVisitAsync(Id);
            this.SetPrompt("نوبت فعال شد");
            return RedirectToPage("VisitListManager", routeValues: new { SelectedDate = SelectedDate });
        }

        public async Task<IActionResult> OnGetDeactivate()
        {
            await _visitRepository.DeactivateVisitAsync(Id);
            this.SetPrompt("نوبت غیر فعال شد");
            return RedirectToPage("VisitListManager", routeValues: new { SelectedDate = SelectedDate });
        }
    }
}
