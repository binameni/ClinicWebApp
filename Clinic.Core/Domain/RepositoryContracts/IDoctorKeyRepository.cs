using Clinic.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.Domain.RepositoryContracts
{
    public interface IDoctorKeyRepository
    {
        ValueTask<EntityEntry<DoctorKey>> SetKeyAsync(DoctorKey key);
        Task<DoctorKey> GetKey();
        Task DeleteKey();
        Task DeleteKeyAndSaveAsync();
        Task SaveAsync();
    }
}
