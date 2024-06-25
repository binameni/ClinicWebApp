using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.Domain.Entities
{
    public class DoctorKey
    {
        [Key]
        public string Key { get; set; }
    }
}
