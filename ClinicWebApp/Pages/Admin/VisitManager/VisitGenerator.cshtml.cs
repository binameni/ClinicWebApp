using Clinic.Core.Domain.Entities;
using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using ClinicWebApp.Extensions.CalendarExtensions;
using ClinicWebApp.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace ClinicWebApp.Pages.Admin.VisitManager
{
    [Authorize(Roles = nameof(ApplicationRoles.Doctor))]
    public class VisitGeneratorModel(IVisitRepository visitRepository, 
        UserManager<ApplicationUser> userManager,
        IEmailRepository emailRepository,
        IDoctorVisitRepository doctorVisitRepository) : PageModelWrapper
    {
        private readonly IVisitRepository _visitRepository = visitRepository;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IEmailRepository _emailRepository = emailRepository;
        private readonly IDoctorVisitRepository _doctorVisitRepository = doctorVisitRepository;

        [FromQuery]
        public string SelectedDate { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "وارد کردن فاصله نوبت ها اجباری است")]
        [Display(Name = "فاصله نوبت ها")]
        public double TimeDifference { get; set; } = 15;

        [BindProperty]
        [Required(ErrorMessage = "وارد کردن شروع شیفت کاری اجباری است")]
        [Display(Name = "ساعت شروع کار")]
        public string StartTime { get; set; } = "8:00";

        [BindProperty]
        [Required(ErrorMessage = "وارد کردن پایان شیفت کاری اجباری است")]
        [Display(Name = "ساعت پایان کار")]
        public string StopTime { get; set; } = "20:00";

        [BindProperty]
        [Required(ErrorMessage = "اگر تمایل به قیمت گذاری نداری، قیمت را صفر وارد کنید")]
        [Display(Name = "هزینه")]
        public int Price { get; set; } = 0;

        [BindProperty]
        [Required(ErrorMessage = "وارد کردن عنوان اجباری است")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (!await _visitRepository.ExistAny(SelectedDate))
                {
                    List<Visit> visits = new List<Visit>();
                    TimeOnly currentDate = TimeOnly.Parse(StartTime);
                    while (currentDate <= TimeOnly.Parse(StopTime))
                    {
                        visits.Add(new()
                        {
                            Date = SelectedDate,
                            Time = currentDate.ToFormat24h(),
                            IsActive = true,
                            IsTaken = false,
                            Title = Title,
                            IsFree = Price == 0,
                            Price = Price,
                            TimeDifference = TimeDifference,
                        });
                        currentDate = currentDate.AddMinutes(TimeDifference);
                    }
                    await _visitRepository.AddVisitAsync(visits);
                    await _visitRepository.SaveChangesAsync();

                    ApplicationUser doctor = await _userManager.FindByNameAsync(User.Identity.Name);
                    if (doctor.Visits.IsNullOrEmpty())
                    {
                        doctor.Visits = new();
                    }
                    doctor.Visits.AddRange(visits);
                    await _userManager.UpdateAsync(doctor);

                    this.SetPrompt("نوبت ها ایجاد شدند.");

                    var users = await _doctorVisitRepository.GetDoctorsPatients(User.Identity.Name);
                    foreach (var u in users)
                    {
                        if (u.Email == null)
                        {
                            continue;
                        }

                        await _emailRepository.Send(u.Email, $"نوبت جدید دکتر {User.Identity.Name}"
                            , $"سلام آقا/خانم {u.Name} نوبت های جدیدی در تاریخ {SelectedDate} برای دکتر {User.Identity.Name} ایجاد شد.");
                    }
                }
                else
                {
                    this.SetPrompt("برای ساخت مجدد برنامه نوبت ها، نوبت های قبلی را پاک کنید.");
                }

                return RedirectToPage("VisitListManager", routeValues: new { SelectedDate = SelectedDate });
            }

            return Page();
        }
    }
}
