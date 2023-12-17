using HR.LeaveManagment.Application.Contracts.Infrastructure;
using HR.LeaveManagment.Application.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Infrastrucure.SendEmail
{
    public class SendEmail : IEmailSender
    {
        private  EmailSettings _Emailsettings { get; }
        public SendEmail(IOptions<EmailSettings> emailsettings)
        {
            _Emailsettings = emailsettings.Value;
        }
        public async Task<bool> Sendemail(Email email)
        {
            var Client = new SendGridClient(_Emailsettings.ApiKey);
            var To = new EmailAddress(email.To);
            var From = new EmailAddress
            {
                Email = _Emailsettings.FromAddress,
                Name = _Emailsettings.FromName
            };
            var Message = MailHelper.CreateSingleEmail(From,To,email.Subject,email.Body,email.Body);
            var response = await Client.SendEmailAsync(Message);
            return response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }
    }
}
