using Clinic.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.Domain.RepositoryContracts
{
    public interface IDoctorRepository
    {
        ValueTask<EntityEntry<Doctor>> AddAsync(Doctor entity);
        Task UpdateAndSaveAsync(Doctor entity);
        Task<Doctor> Get(string username);
        Task<List<Doctor>> Get(List<string> username);
        Task SaveAsync();
        Task<bool> Exist(string username);
    }
}
