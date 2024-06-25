using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using ClinicWebApp.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicWebApp.Pages.Admin.VisitManager
{
    [Authorize(Roles = nameof(ApplicationRoles.Doctor))]
    public class ChangePaymentModel(IVisitRepository visitRepository) : PageModelWrapper
    {
        private readonly IVisitRepository _visitRepository = visitRepository;

        [FromQuery]
        public int Id { get; set; }

        [FromQuery]
        public int Price { get; set; } = 0;

        [FromQuery]
        public string SelectedDate { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnGetSetPayment()
        {
            if (Price != 0)
            {
                await _visitRepository.SetPayment(Id, Price);
                this.SetPrompt("نوبت قیمت گذاری شد");
            }
            else
            {
                this.SetPrompt("نوبت از قبل فیمت گذاری شده است");
            }
            return RedirectToPage("VisitListManager", routeValues: new { SelectedDate = SelectedDate });
        }

        public async Task<IActionResult> OnGetSetFree()
        {
            await _visitRepository.SetFree(Id);
            this.SetPrompt("قیمت این نوبت برداشته شد");
            return RedirectToPage("VisitListManager", routeValues: new { SelectedDate = SelectedDate });
        }
    }
}
