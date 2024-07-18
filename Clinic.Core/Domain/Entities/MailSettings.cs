using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.Domain.Entities
{
    public class MailSettings
    {
        public string Server { get; set; }
        public int? Port { get; set; }
        public string From { get; set; }
        public string? DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
