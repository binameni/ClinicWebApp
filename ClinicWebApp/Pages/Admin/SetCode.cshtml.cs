using Clinic.Core.Converter;
using Clinic.Core.Domain.Entities;
using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using ClinicWebApp.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ClinicWebApp.Pages.Admin
{
    [Authorize(Roles = nameof(ApplicationRoles.Doctor))]
    public class SetCodeModel(IDoctorKeyRepository doctorKeyRepository) : PageModelWrapper
    {
        private readonly IDoctorKeyRepository _doctorKeyRepository = doctorKeyRepository;

        [BindProperty]
        [Required(ErrorMessage = "لطفا کد قدیمی را وارد کنید")]
        [Display(Name = "کد قدیمی")]
        public string OldCode { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "لطفا کد جدید را وارد کنید")]
        [Display(Name = "کد جدید")]
        public string Code { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                string hashedOldCode = OldCode.Hash();
                if (hashedOldCode == (await _doctorKeyRepository.GetKey()).Key)
                {
                    await _doctorKeyRepository.DeleteKey();
                    await _doctorKeyRepository.SetKeyAsync(new DoctorKey { Key = Code.Hash() });
                    await _doctorKeyRepository.SaveAsync();
                    // Set doctors' access to normal users !
                    SetPrompt("کد با موفقیت ثبت شد");
                    return RedirectToPage("../Index");
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
