using Clinic.Core.Domain.Entities;
using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using ClinicWebApp.Pages.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace ClinicWebApp.Pages
{
    public class IndexModel(UserManager<ApplicationUser> userManager, IDoctorRepository doctorRepository,
        ILogger<IndexModel> logger) : PageModelWrapper
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IDoctorRepository _doctorRepository = doctorRepository;
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public List<(ApplicationUser, Doctor)> Doctors { get; set; } = new();

        [BindProperty]
        public int DoctorCount { get; set; }

        public async Task OnGet()
        {
            var doctors = (await _userManager.GetUsersInRoleAsync(ApplicationRoles.Doctor)).Take(3).ToList();
            List<string> usernames = doctors.Select(doctor => doctor.UserName).ToList();
            List<Doctor> doctorsInfo = await _doctorRepository.Get(usernames);
            if (!doctors.IsNullOrEmpty())
            {
                for (int i = 0; i < doctors.Count; i++)
                {
                    Doctors.Add((doctors[i], doctorsInfo[i]));
                }
            }

            DoctorCount = await _doctorRepository.CountAllAsync();
        }
    }
}
