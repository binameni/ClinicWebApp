using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.Domain.RepositoryContracts
{
    public interface IEmailRepository
    {
        Task Send(string reciever, string subject, string text);
    }
}
