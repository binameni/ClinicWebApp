using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.Domain.IdentityEntities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
    }

    public struct ApplicationRoles
    {
        public static readonly string Doctor = "Doctor";
        public static readonly string Patient = "Patient";
    }
}
