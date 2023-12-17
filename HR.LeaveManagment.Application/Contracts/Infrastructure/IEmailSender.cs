using HR.LeaveManagment.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.Contracts.Infrastructure
{
    public interface IEmailSender
    {
        Task<bool> Sendemail(Email email);
    }
}
