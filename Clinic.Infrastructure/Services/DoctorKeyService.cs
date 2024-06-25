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
    public class DoctorKeyService : IDoctorKeyRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorKeyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task DeleteKey()
        {
            var key = await _context.DoctorKey.FirstOrDefaultAsync();
            if (key != null)
            {
                _context.DoctorKey.Remove(key);
            }
        }

        public async Task DeleteKeyAndSaveAsync()
        {
            await this.DeleteKey();
            await _context.SaveChangesAsync();
        }

        public Task<DoctorKey> GetKey()
        {
            return _context.DoctorKey.FirstOrDefaultAsync();
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public ValueTask<EntityEntry<DoctorKey>> SetKeyAsync(DoctorKey key)
        {
            return _context.AddAsync(key);
        }
    }
}
