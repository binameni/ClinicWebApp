using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using Clinic.Infrastructure.DbContext;
using Clinic.Infrastructure.Services;
using ClinicWebApp.BackgroundJobs.Visits;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace ClinicWebApp.Extensions.StartupExtensions
{
    public static class ServicesExtension
    {
        public static void ConfigureServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IVisitRepository, VisitService>();
            builder.Services.AddTransient<IDoctorKeyRepository, DoctorKeyService>();
            builder.Services.AddScoped<IDoctorRepository, DoctorService>();
            builder.Services.AddTransient<IUserVisitRepository, UserVisitService>();
            builder.Services.AddTransient<IDoctorVisitRepository, DoctorVisitService>();
        }

        public static void ConfigureUserCredentials(this IHostApplicationBuilder builder)
        {
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddErrorDescriber<PersianIdentityErrorDescriber>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            });

            builder.Services.AddAuthorization();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Signin";
            });
        }

        public static void ConfigureDbContext(this IHostApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDB"));
            });
        }

        public static void ConfigureQuartz(this IHostApplicationBuilder builder)
        {
            builder.Services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();
            });

            builder.Services.AddQuartzHostedService();

            builder.Services.ConfigureOptions<VisitCleaningBackgroundJobSetup>();
        }
    }
}
