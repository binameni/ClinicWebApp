using Clinic.Core.Domain.Entities;
using Clinic.Core.Domain.RepositoryContracts;
using Clinic.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Services
{
    public class DoctorService : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ValueTask<EntityEntry<Doctor>> AddAsync(Doctor entity)
        {
            return _context.Doctors.AddAsync(entity);
        }

        public async Task<bool> Exist(string username)
        {
            return await _context.Doctors.CountAsync(x => x.DoctorNumber == username) > 0;
        }

        public Task<Doctor> Get(string username)
        {
            return _context.Doctors.FirstOrDefaultAsync(x=>x.DoctorNumber == username);
        }

        public async Task<List<Doctor>> Get(List<string> username)
        {
            List<Doctor> result = new List<Doctor>();
            foreach (var item in username)
            {
                result.Add(await _context.Doctors.FirstOrDefaultAsync(x => x.DoctorNumber == item));
            }
            return result;
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task UpdateAndSaveAsync(Doctor entity)
        {
            _context.Update(entity);
            return _context.SaveChangesAsync();
        }
    }
}
