using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using ClinicWebApp.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace ClinicWebApp.Pages.Account
{
    [Authorize]
    public class DeleteModel(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserVisitRepository userVisitRepository) : PageModelWrapper
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IUserVisitRepository _userVisitRepository = userVisitRepository;

        public async Task<IActionResult> OnGet()
        {
            ApplicationUser user = await _userVisitRepository.GetUserAndVisits(User.Identity.Name);
            if (user.Visits.IsNullOrEmpty())
            {
                await _signInManager.SignOutAsync();
                await _userManager.DeleteAsync(user);
                this.SetPrompt("حساب کاربری با موفقیت حذف شد");
                return RedirectToPage("../Index");
            }
            else
            {
                this.SetPrompt("قبل از حذف حساب باید نوبت های خود را حذف کنید");
                if (await _userManager.IsInRoleAsync(user, ApplicationRoles.Doctor))
                {
                    return RedirectToPage("../Admin/VisitManager/VisitListManager");
                }
                else
                {
                    // هدایت به سمت نوبت های خریداری شده توسط بیمار
                    return RedirectToPage();
                }
            }
        }
    }
}
