using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.Domain.Entities
{
    public class Doctor
    {
        [Key]
        public string DoctorNumber { get; set; }
        public bool IsRegistered { get; set; }
        public string Profession { get; set; }
        public string? ProfileSrc { get; set; }
        public string? ProfilePhysical { get; set; }
        public List<string>? PicturesSrc { get; set; }
        public List<string>? PicturesPhysical { get; set; }
    }
}
