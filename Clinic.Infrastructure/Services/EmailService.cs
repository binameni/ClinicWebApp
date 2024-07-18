using Clinic.Core.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Clinic.Core.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Clinic.Infrastructure.Services
{
    public class EmailService : IEmailRepository
    {
        private readonly MailSettings _settings;
        public EmailService(IOptions<MailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task Send(string reciever, string subject, string text)
        {
            MailAddress to = new MailAddress(reciever);
            MailAddress from = new MailAddress(_settings.From);
            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = text;
            SmtpClient client = new SmtpClient(_settings.Server);
            if (_settings.Port != 0)
            {
                client.Port = (int)_settings.Port;
            }
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_settings.Username, _settings.Password);
            await client.SendMailAsync(message);
        }
    }
}
