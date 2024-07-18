using Clinic.Core.Domain.Entities;
using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using ClinicWebApp.Extensions.CalendarExtensions;
using ClinicWebApp.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace ClinicWebApp.Pages.User.VisitReserver
{
    [Authorize(Roles = nameof(ApplicationRoles.Patient))]
    public class VisitListReserverModel(IVisitRepository visitRepository, IDoctorVisitRepository doctorVisitRepository) : PageModelWrapper
    {
        private readonly IVisitRepository _visitRepository = visitRepository;
        private readonly IDoctorVisitRepository _doctorVisitRepository = doctorVisitRepository;

        public List<string> Dates { get; set; } = new();

        [FromQuery]
        public string DoctorUserName { get; set; }

        [FromQuery]
        public string? SelectedDate { get; set; }

        public List<Visit> Visits { get; set; } = new List<Visit>();

        public async Task OnGet()
        {
            Dates.AddRange(await _doctorVisitRepository.GetDatesAsync(DoctorUserName));
            if (SelectedDate.IsNullOrEmpty())
            {
                SelectedDate = Dates.FirstOrDefault();
            }
            Visits.AddRange(await _doctorVisitRepository.GetVisitsAsync(SelectedDate, DoctorUserName));
            //Visits.ForEach(x =>
            //{
            //    if (TimeSpan.Parse(x.Time) < DateTime.UtcNow.ToTimeSpan())
            //    {
            //        x.IsActive = false;
            //    }
            //});
            foreach (var x in Visits)
            {
                if (x.Date == DateTime.UtcNow.ToPersianString() &&
                    TimeSpan.Parse(x.Time) < DateTime.UtcNow.ToPersianTimeSpan())
                {
                    x.IsActive = false;
                }
            }
        }
    }
}
