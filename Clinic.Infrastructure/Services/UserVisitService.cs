using Clinic.Core.Domain.Entities;
using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using Clinic.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
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

        public async Task ReserveVisit(int visitId, string userName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            Visit visit = await _visitRepository.FindAsync(visitId);

            if (user.Visits.IsNullOrEmpty())
            {
                user.Visits = new List<Visit>();
            }
            user.Visits.Append(visit);
            visit.IsTaken = true;

            _visitRepository.Update(visit);
            await _userManager.UpdateAsync(user);

            await _context.SaveChangesAsync();
        }

        public Task ShiftVisit(int oldVisitId, int newVisitId, string userName)
        {
            throw new NotImplementedException();
        }
    }
}
