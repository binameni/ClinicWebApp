using Clinic.Core.Domain.Entities;
using Clinic.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorKey> DoctorKey { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var doctorRole = new ApplicationRole
            {
                Id = Guid.NewGuid(),
                Name = ApplicationRoles.Doctor,
                NormalizedName = ApplicationRoles.Doctor.Normalize(),
            };

            var patientRole = new ApplicationRole
            {
                Id = Guid.NewGuid(),
                Name = ApplicationRoles.Patient,
                NormalizedName = ApplicationRoles.Patient.Normalize(),
            };

            builder.Entity<ApplicationRole>(x =>
            {
                x.HasData(doctorRole, patientRole);
                x.ToTable("AspNetRoles", "dbo");
            });

            //builder.Entity<ApplicationRole>().HasData(doctorRole, patientRole);

            //builder.HasDefaultSchema("clinicIdentity");
        }
    }
}
