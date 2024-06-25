using Clinic.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; }

        [MaxLength(10)]
        public string NationalCode { get; set; }
        public List<Visit>? Visits { get; set; }
        public Doctor? DoctorInfo { get; set; }
    }
}
