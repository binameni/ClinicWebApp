using Clinic.Core.Domain.IdentityEntities;
using ClinicWebApp.Pages.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace ClinicWebApp.Pages.Account
{
    public class DeleteModel(UserManager<ApplicationUser> userManager) : PageModelWrapper
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<IActionResult> OnGet()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            // ویزیت همیشه نال میاد
            // include
            if (user.Visits.IsNullOrEmpty())
            {
                await _userManager.DeleteAsync(user);
                this.SetPrompt("حساب کاربری با موفقیت حذف شد");
                return RedirectToPage("../../Index");
            }
            else
            {
                this.SetPrompt("قبل از حذف حساب باید نوبت های خود را حذف کنید");
                if (await _userManager.IsInRoleAsync(user, ApplicationRoles.Doctor))
                {
                    return RedirectToPage("../../Admin/VisitManager/VisitListManager");
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
