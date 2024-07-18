using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using ClinicWebApp.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicWebApp.Pages.User.VisitReserver
{
    [Authorize(Roles = nameof(ApplicationRoles.Patient))]
    public class BuyVisitModel(IUserVisitRepository userVisitRepository) : PageModelWrapper
    {
        private readonly IUserVisitRepository _userVisitRepository = userVisitRepository;

        [FromQuery]
        public int Id { get; set; }

        [FromQuery]
        public string SelectedDate { get; set; }

        [FromQuery]
        public string DoctorUserName { get; set; }

        public async Task<IActionResult> OnGetRequest()
        {
            return RedirectToPage("BuyVisit", pageHandler: "Validate", routeValues: new
            {
                Id = Id,
                SelectedDate = SelectedDate,
                DoctorUserName =DoctorUserName
            });
        }

        public async Task<IActionResult> OnGetValidate(/*string authority, string status*/)
        {
            this.SetPrompt("خرید با موفقیت انجام شد");
            return RedirectToPage("ReserveVisit", routeValues: new
            {
                Id = Id,
                SelectedDate = SelectedDate,
                DoctorUserName = DoctorUserName
            });
        }

        //public async Task<IActionResult> OnGetRefreshAuthority()
        //{
        //}

        //public async Task<IActionResult> OnGetUnverified()
        //{
        //}
    }
}
