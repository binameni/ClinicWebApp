using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using ClinicWebApp.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicWebApp.Pages.User.VisitReserver
{
    [Authorize(Roles = nameof(ApplicationRoles.Patient))]
    public class ReserveVisitModel(IUserVisitRepository userVisitRepository) : PageModelWrapper
    {
        private readonly IUserVisitRepository _userVisitRepository = userVisitRepository;

        [FromQuery]
        public int Id { get; set; }

        [FromQuery]
        public string SelectedDate { get; set; }

        [FromQuery]
        public string DoctorUserName { get; set; }

        public async Task<IActionResult> OnGet()
        {
            await _userVisitRepository.ReserveVisit(Id, User.Identity.Name);
            this.SetPrompt("نوبت رزرو شد");
            return RedirectToPage("VisitListReserver", routeValues: new
            {
                SelectedDate = SelectedDate,
                DoctorUserName = DoctorUserName
            });
        }
    }
}
