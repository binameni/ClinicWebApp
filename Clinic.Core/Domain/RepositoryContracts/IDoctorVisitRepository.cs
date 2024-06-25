using Clinic.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.Domain.RepositoryContracts
{
    public interface IDoctorVisitRepository
    {
        Task<List<string>> GetDatesAsync(string doctorUserName);
        Task<List<Visit>> GetVisitsAsync(string date, string doctorUserName);
    }
}
