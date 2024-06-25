using Clinic.Core.Domain.Entities;
using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using ClinicWebApp.Extensions.CalendarExtensions;
using ClinicWebApp.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

namespace ClinicWebApp.Pages.Admin.VisitManager
{
    [Authorize(Roles = nameof(ApplicationRoles.Doctor))]
    public class VisitListManagerModel(IVisitRepository visitRepository) : PageModelWrapper
    {
        private readonly IVisitRepository _visitRepository = visitRepository;

        public List<string> Dates { get; set; } = new();

        [FromQuery]
        public string? SelectedDate { get; set; }

        public List<Visit> Visits { get; set; } = new List<Visit>();

        public async Task OnGet()
        {
            DateTime currentDate = DateTime.UtcNow.ToPersianDateTime();

            if (SelectedDate.IsNullOrEmpty())
            {
                SelectedDate = currentDate.ToDateString();
            }

            for (int i = 0; i < 7; i++)
            {
                Dates.Add(currentDate.ToDateString());
                currentDate = currentDate.AddDays(1);
            }

            if (Visits.IsNullOrEmpty())
            {
                var todayVisit = await _visitRepository.GetAsync(SelectedDate);
                if (todayVisit != null)
                {
                    Visits.AddRange(todayVisit);
                }
            }
        }
    }
}
