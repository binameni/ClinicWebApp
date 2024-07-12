using Clinic.Core.Domain.RepositoryContracts;
using ClinicWebApp.Extensions.CalendarExtensions;
using Quartz;

namespace ClinicWebApp.BackgroundJobs.Visits
{
    [DisallowConcurrentExecution]
    public class VisitCleaningBackgroundJob : IJob
    {
        private readonly IVisitRepository _visitRepository;

        public VisitCleaningBackgroundJob(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return _visitRepository.ClearBefore(DateTime.UtcNow.ToPersianDateTime());
        }
    }
}
