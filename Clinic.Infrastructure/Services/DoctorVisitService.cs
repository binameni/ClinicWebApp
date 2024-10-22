﻿using Clinic.Core.Domain.Entities;
using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using Clinic.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Services
{
    public class DoctorVisitService : IDoctorVisitRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DoctorVisitService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<string>> GetDatesAsync(string doctorUserName)
        {
            ApplicationUser doctor = await _userManager.Users.Include(x => x.Visits)
                .FirstOrDefaultAsync(x => x.UserName == doctorUserName);
            return doctor.Visits.Select(x => x.Date).Distinct().ToList();
        }

        public Task<List<ApplicationUser>> GetDoctorsPatients(string username)
        {
            return _context.Users
                .Where(user => user.UserName == username && user.Visits.Any(visit => !visit.IsFree))
                .SelectMany(user => user.Visits)
                .SelectMany(visit => visit.Users)
                .Where(user => user.DoctorInfo == null)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<Visit>> GetVisitsAsync(string date, string doctorUserName)
        {
            ApplicationUser doctor = await _userManager.Users.Include(x => x.Visits)
                .FirstOrDefaultAsync(x => x.UserName == doctorUserName);
            return doctor.Visits.OrderBy(x => DateTime.Parse(x.Time)).Where(x => x.Date == date).ToList();
        }
    }
}
