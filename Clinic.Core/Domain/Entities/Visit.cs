using Clinic.Core.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.Domain.Entities
{
    public class Visit
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [MaxLength(10)]
        public string Date { get; set; }

        [MaxLength(5)]
        public string Time { get; set; }

        [DefaultValue(false)]
        public bool IsTaken { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }

        [DefaultValue(true)]
        public bool IsFree { get; set; }

        [DefaultValue(0)]
        public int Price { get; set; }

        public List<ApplicationUser>? Users { get; set; }
    }
}
