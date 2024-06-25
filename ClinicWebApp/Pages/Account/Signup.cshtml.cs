using Clinic.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace ClinicWebApp.Pages.Account
{
    public class SignupModel(SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager) : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

        [BindProperty]
        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        [Display(Name = "نام کاربری (کد ملی)")]
        [Length(minimumLength: 10, maximumLength: 10, ErrorMessage = "کد ملی باید ده رقم باشد")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "نام خود را وارد کنید")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "موبایل خود را وارد کنید")]
        [Display(Name = "شماره موبایل")]
        [Length(minimumLength: 10, maximumLength: 10, ErrorMessage = "شماره موبایل باید ده رقم باشد")]
        public string PhoneNumber { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "رمز خود را وارد کنید")]
        [Display(Name = "رمز ورود")]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "تکرار رمز را وارد کنید")]
        [Display(Name = "تکرار رمز ورود")]
        public string ConfirmPassword { get; set; }

        [FromQuery]
        public string? ReturnUrl { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!ulong.TryParse(Username, out var temp1))
            {
                ModelState.AddModelError(nameof(Username), "لطفا در نام کاربری کد ملی خود را وارد کنید");
                return Page();
            }

            if (!ulong.TryParse(PhoneNumber, out var temp2))
            {
                ModelState.AddModelError(nameof(PhoneNumber), "لطفا در قسمت شماره موبایل عدد وارد کنید");
                return Page();
            }

            if (Password != ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "رمز ورود با تکرار آن برابر نیست");
                return Page();
            }

            var user = new ApplicationUser
            {
                UserName = Username,
                Name = Name,
                NationalCode = Username,
                PhoneNumber = PhoneNumber
            };

            IdentityResult creationResult = await _userManager.CreateAsync(user, Password);

            if (!creationResult.Succeeded)
            {
                foreach (var error in creationResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    return Page();
                }
            }

            await _userManager.AddToRoleAsync(user, ApplicationRoles.Patient);
            await _userManager.UpdateAsync(user);

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(Username, Password, true, false);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "ورود کاربر با خطا مواج شد");
                return RedirectToPage("Signin");
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
    }
}
