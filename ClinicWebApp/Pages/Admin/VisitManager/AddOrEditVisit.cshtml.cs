using Clinic.Core.Domain.RepositoryContracts;
using ClinicWebApp.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Clinic.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Clinic.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Clinic.Infrastructure.Services;
using System.Net.Mail;
using System.Net;

namespace ClinicWebApp.Pages.Admin.VisitManager
{
    [Authorize(Roles = nameof(ApplicationRoles.Doctor))]
    public class EditVisitModel(IVisitRepository visitRepository, 
        UserManager<ApplicationUser> userManager,
        IEmailRepository emailRepository,
        IDoctorVisitRepository doctorVisitRepository) : PageModelWrapper
    {
        private readonly IVisitRepository _visitRepository = visitRepository;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IEmailRepository _emailRepository = emailRepository;
        private readonly IDoctorVisitRepository _doctorVisitRepository = doctorVisitRepository;

        [FromQuery]
        [Display(Name = "آیدی نوبت")]
        public int Id { get; set; }

        [FromQuery]
        [Display(Name = "تاریخ منتخب")]
        public string SelectedDate { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "عنوان نوبت را وارد کنید")]
        [Display(Name = "عنوان نوبت")]
        public string Title { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "ساعت نوبت را وارد کنید")]
        [Display(Name = "ساعت نوبت")]
        public string Time { get; set; }

        [BindProperty]
        [Display(Name = "وضعیت نوبت (فعال/غیر فعال)")]
        public bool IsActive { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "قیمت را وارد کنید (در صورت رایگان بودن 0 قرار دهید")]
        [Display(Name = "هزینه")]
        public int Price { get; set; } = 0;

        [BindProperty]
        [Required(ErrorMessage = "وارد کردن مدت زمان نوبت اجباری است")]
        [Display(Name = "مدت زمان نوبت")]
        public double TimeDifference { get; set; } = 15;

        public async Task OnGet()
        {
            if (Id != 0)
            {
                Visit visit = await _visitRepository.FindAsync(Id);
                Title = visit.Title;
                Time = visit.Time;
                IsActive = visit.IsActive;
                Price = visit.Price;
                SelectedDate = visit.Date;
                TimeDifference = visit.TimeDifference;
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                Visit visit = new Visit();
                if (Id != 0)
                {
                    visit = await _visitRepository.FindAsync(Id);
                }
                visit.Title = Title;
                visit.Time = Time;
                visit.IsActive = IsActive;
                visit.TimeDifference = TimeDifference;
                if (Price > 0)
                {
                    visit.IsFree = false;
                }
                else
                {
                    visit.IsFree = true;
                }
                visit.Price = Price;
                if (Id != 0)
                {
                    _visitRepository.Update(visit);
                    this.SetPrompt("نوبت آپدیت شد");
                }
                else
                {
                    visit.Date = SelectedDate;
                    await _visitRepository.AddVisitAsync(visit);

                    ApplicationUser doctor = await _userManager.FindByNameAsync(User.Identity.Name);
                    if (doctor.Visits.IsNullOrEmpty())
                    {
                        doctor.Visits = new List<Visit>();
                    }
                    doctor.Visits.Add(visit);
                    await _userManager.UpdateAsync(doctor);

                    this.SetPrompt("نوبت ساخته شد");
                }
                await _visitRepository.SaveChangesAsync();

                // Newly created visit's do not have Id and when you save them they get one,
                // so I collected it to pass into redirect
                Id = visit.Id;
            }

            var users = await _doctorVisitRepository.GetDoctorsPatients(User.Identity.Name);
            foreach (var u in users)
            {
                if (u.Email == null)
                {
                    continue;
                }

                await _emailRepository.Send(u.Email, $"نوبت جدید دکتر {User.Identity.Name}"
                    , $"سلام آقا/خانم {u.Name} نوبت جدیدی در تاریخ {SelectedDate} و ساعت {Time} برای دکتر {User.Identity.Name} ایجاد شد.");
            }

            return RedirectToPage("AddOrEditVisit", routeValues: new { Id = Id, SelectedDate = SelectedDate });
        }
    }
}
