using Clinic.Core.Domain.Entities;
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
    public class SetupInfoModel(IDoctorRepository doctorRepository,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager) : PageModelWrapper
    {
        private readonly IDoctorRepository _doctorRepository = doctorRepository;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

        [BindProperty]
        [Required(ErrorMessage = "شماره پزشکی را وارد کنید")]
        [Display(Name = "شماره پزشکی")]
        public string DoctorNumber { get; set; }

        [BindProperty]
        [Display(Name = "تخصص")]
        [Required(ErrorMessage = "تخصص را وارد کنید")]
        public string Profession { get; set; }

        public async Task OnGet()
        {
            Doctor doctor = await _doctorRepository.Get(User.Identity.Name);
            if (doctor != null)
            {
                DoctorNumber = doctor.DoctorNumber;
                Profession = doctor.Profession;
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                // Creates a doctor object to store in Doctor table
                var doctor = new Doctor()
                {
                    DoctorNumber = DoctorNumber,
                    IsRegistered = true,
                    Profession = Profession
                };

                // Recieves ApplicationUser related to the doctor to change the "username" to "DoctorNumber" instead of "NationalCode"
                ApplicationUser userDoctor = await _userManager.FindByNameAsync(User.Identity.Name);
                userDoctor.UserName = doctor.DoctorNumber;

                if (await _doctorRepository.Exist(User.Identity.Name))
                {
                    await _doctorRepository.UpdateAndSaveAsync(doctor);
                }
                else
                {
                    await _doctorRepository.AddAsync(doctor);
                    userDoctor.DoctorInfo = doctor;
                }

                await _userManager.UpdateAsync(userDoctor);
                await _userManager.AddToRoleAsync(userDoctor, ApplicationRoles.Doctor);

                // To reload the user cedentials
                await _signInManager.SignOutAsync();

                this.SetPrompt("اکانت شما به دسترسی ادمین مجاز شد. لطفا مجدد وارد شوید. \nنام کاربری شما از این پس شماره پزشکی شماست.");
                return RedirectToPage("../Account/Signin");
            }

            return Page();
        }
    }
}
