using Clinic.Core.Domain.IdentityEntities;
using ClinicWebApp.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Xml;

namespace ClinicWebApp.Pages.Account
{
    public class SigninModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : PageModelWrapper
    {
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        [BindProperty]
        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        [Display(Name = "نام کاربری (کد ملی)")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "رمز ورود را وارد کنید")]
        [Display(Name = "رمز ورود")]
        public string Password { get; set; }

        [FromQuery]
        public string? ReturnUrl { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(Username, Password, true, false);
                if (!signInResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "از صحت اطلاعات ورودی مطمئن شوید");
                    return Page();
                }

                ApplicationUser applicationUser = await _userManager.FindByNameAsync(Username);
                var roles = await _userManager.GetRolesAsync(applicationUser);
                if (roles.IsNullOrEmpty())
                {
                    await _userManager.AddToRoleAsync(applicationUser, ApplicationRoles.Patient);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(applicationUser, isPersistent: true);
                }

                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return LocalRedirect(ReturnUrl);
                }
                else
                {
                    return RedirectToPage("../Index");
                }
            }

            return Page();
        }
    }
}
