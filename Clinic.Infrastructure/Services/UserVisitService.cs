using Clinic.Core.Domain.Entities;
using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using Clinic.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Services
{
    public class UserVisitService : IUserVisitRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVisitRepository _visitRepository;

        public UserVisitService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IVisitRepository visitRepository)
        {
            _context = context;
            _userManager = userManager;
            _visitRepository = visitRepository;
        }

        public Task<ApplicationUser> GetUserAndVisits(string username)
        {
            return _context.Users.Include(x => x.Visits).FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task ReserveVisit(int visitId, string userName)
        {
            ApplicationUser user = await _context.Users.Include(x => x.Visits).FirstOrDefaultAsync(x => x.UserName == userName);
            Visit visit = await _visitRepository.FindAsync(visitId);

            if (visit == null)
            {
                return;
            }

            if (!visit.IsActive || visit.IsTaken)
            {
                return;
            }

            if (user.Visits.IsNullOrEmpty())
            {
                user.Visits = new List<Visit>();
            }
            user.Visits.Add(visit);
            visit.IsTaken = true;

            _visitRepository.Update(visit);
            await _context.SaveChangesAsync();

            await _userManager.UpdateAsync(user);
        }
    }
}
