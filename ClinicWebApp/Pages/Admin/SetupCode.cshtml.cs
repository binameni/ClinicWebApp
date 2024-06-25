using Clinic.Core.Converter;
using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using ClinicWebApp.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ClinicWebApp.Pages.Admin
{
    [Authorize]
    public class SetupCodeModel(IDoctorKeyRepository doctorKeyRepository) : PageModelWrapper
    {
        private readonly IDoctorKeyRepository _doctorKeyRepository = doctorKeyRepository;

        [BindProperty]
        [Required(ErrorMessage = "لطفا کد را وارد کنید")]
        [Display(Name = "کد")]
        public string Code { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                string hashedCode = Code.Hash();
                if (hashedCode == (await _doctorKeyRepository.GetKey()).Key)
                {
                    this.SetPrompt("کد ورود تایید شد");
                    return RedirectToPage("SetupInfo");
                }
                else
                {
                    ModelState.AddModelError("", "کد ورودی اشتباه هست");
                }
            }

            return Page();
        }
    }
}
