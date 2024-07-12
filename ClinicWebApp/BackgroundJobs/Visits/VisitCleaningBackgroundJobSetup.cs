using Microsoft.Extensions.Options;
using Quartz;

namespace ClinicWebApp.BackgroundJobs.Visits
{
    public class VisitCleaningBackgroundJobSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var jobKey = JobKey.Create(nameof(VisitCleaningBackgroundJobSetup));
            options.AddJob<VisitCleaningBackgroundJob>(jobBuiler => jobBuiler.WithIdentity(jobKey))
                .AddTrigger(trigger => trigger.ForJob(jobKey)
                .StartNow()
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(21,35)
                .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Asia/Tehran"))));
        }
    }
}
