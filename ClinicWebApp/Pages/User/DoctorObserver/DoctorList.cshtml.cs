using Clinic.Core.Domain.Entities;
using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using ClinicWebApp.Pages.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace ClinicWebApp.Pages.User.DoctorObserver
{
    public class DoctorListModel(UserManager<ApplicationUser> userManager, IDoctorRepository doctorRepository) : PageModelWrapper
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IDoctorRepository _doctorRepository = doctorRepository;

        [BindProperty]
        public List<(ApplicationUser, Doctor)> Doctors { get; set; } = new();

        public async Task OnGet()
        {
            var doctors = (await _userManager.GetUsersInRoleAsync(ApplicationRoles.Doctor));
            List<string> usernames = doctors.Select(doctor => doctor.UserName).ToList();
            List<Doctor> doctorsInfo = await _doctorRepository.Get(usernames);
            if (!doctors.IsNullOrEmpty())
            {
                for (int i = 0; i < doctors.Count; i++)
                {
                    Doctors.Add((doctors[i], doctorsInfo[i]));
                }
            }
            else
            {
                this.SetPrompt("دکتری یافت نشد");
            }
        }
    }
}
