using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using Clinic.Infrastructure.DbContext;
using Clinic.Infrastructure.Services;
using ClinicWebApp.Validators.PasswordValidators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
            //builder.Services.AddTransient<SimplePasswordValidator>();
        }

        public static void ConfigureUserCredentials(this IHostApplicationBuilder builder)
        {
            //builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme)
            //    .AddBearerToken(IdentityConstants.BearerScheme);
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddErrorDescriber<PersianIdentityErrorDescriber>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //.AddPasswordValidator<SimplePasswordValidator>(); // error
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
    }
}
