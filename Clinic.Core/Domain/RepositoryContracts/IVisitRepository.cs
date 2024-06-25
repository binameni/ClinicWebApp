using Clinic.Core.Domain.Entities;
using Clinic.Core.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.Domain.RepositoryContracts
{
    public interface IVisitRepository
    {
        ValueTask<EntityEntry<Visit>> AddVisitAsync(Visit visit);
        Task AddVisitAsync(List<Visit> visit);

        //void UpdateVisit(Visit visit);
        void Update(Visit visit);

        Task DeactivateVisitAsync(int id);
        Task ActivateVisitAsync(int id);

        void RemoveVisit(Visit visit);

        Task RemoveAndSave(int id);

        Task<List<Visit>> GetAsync(string date);
        Task<Visit> FindAsync(int id);

        Task SaveChangesAsync();
        Task<bool> ExistAny(string date);
        Task Clear();
        Task Clear(string date);
        Task SetPayment(int id, int price);
        Task SetFree(int id);
    }
}
