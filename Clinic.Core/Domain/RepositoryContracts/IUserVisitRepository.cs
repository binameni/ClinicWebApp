using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.Domain.RepositoryContracts
{
    public interface IUserVisitRepository
    {
        Task ReserveVisit(int visitId, string userName);
        Task ShiftVisit(int oldVisitId, int newVisitId, string userName);
    }
}
