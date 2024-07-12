using Clinic.Core.Domain.Entities;
using Clinic.Core.Domain.IdentityEntities;
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
    public class VisitService : IVisitRepository
    {
        private readonly ApplicationDbContext _context;

        public VisitService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActivateVisitAsync(int id)
        {
            Visit visit = await this.FindAsync(id);
            visit.IsActive = true;
            this.Update(visit);
            await this.SaveChangesAsync();
        }

        public ValueTask<EntityEntry<Visit>> AddVisitAsync(Visit visit)
        {
            return _context.AddAsync(visit);
        }

        public Task AddVisitAsync(List<Visit> visit)
        {
            return _context.AddRangeAsync(visit);
        }

        public Task Clear()
        {
            return _context.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE [Visits]");
        }

        public async Task Clear(string date)
        {
            List<Visit> visits = await _context.Visits.Where(x => x.Date == date).ToListAsync();
            _context.Visits.RemoveRange(visits);
            await this.SaveChangesAsync();
        }

        public async Task ClearBefore(DateTime date)
        {
            int count = await this.CountAllAsync();
            int cycle = count / 10;
            if (count % 10 > 0)
            {
                cycle++;
            }

            for (int i = 0; i < cycle; i++)
            {
                List<Visit> visits = await _context.Visits.Skip(i * 10).Take(10).ToListAsync();
                visits = visits.Where(x => DateTime.Parse(x.Date) < date).ToList();
                if (visits.Count <= 0)
                {
                    continue;
                }
                _context.Visits.RemoveRange(visits);
            }
            await this.SaveChangesAsync();
        }

        public Task<int> CountAllAsync()
        {
            return _context.Visits.CountAsync();
        }

        public async Task DeactivateVisitAsync(int id)
        {
            Visit visit = await this.FindAsync(id);
            visit.IsActive = false;
            this.Update(visit);
            await this.SaveChangesAsync();
        }

        public async Task<bool> ExistAny(string date)
        {
            return await _context.Visits.Where(x => x.Date == date).CountAsync() > 0;
        }

        public Task<Visit> FindAsync(int id)
        {
            return _context.Visits.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Visit>> GetAsync(string date)
        {
            return _context.Visits.Where(x => x.Date == date).OrderBy(x => x.Time).ToListAsync();
        }

        public async Task RemoveAndSave(int id)
        {
            Visit visit = await _context.Visits.FirstOrDefaultAsync(x => x.Id == id);
            if (visit != null)
            {
                this.RemoveVisit(visit);
                await this.SaveChangesAsync();
            }
        }

        public void RemoveVisit(Visit visit)
        {
            _context.Visits.Remove(visit);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task SetFree(int id)
        {
            //return _context.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE [{nameof(Visit)}]");
            Visit visit = await this.FindAsync(id);
            visit.IsFree = true;
            visit.Price = 0;
            this.Update(visit);
            await this.SaveChangesAsync();
        }

        public async Task SetPayment(int id, int price)
        {
            Visit visit = await this.FindAsync(id);
            visit.IsFree = false;
            visit.Price = price;
            this.Update(visit);
            await this.SaveChangesAsync();
        }

        public void Update(Visit visit)
        {
            _context.Visits.Update(visit);
        }
    }
}
