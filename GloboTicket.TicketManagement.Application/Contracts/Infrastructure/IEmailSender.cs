using GloboTicket.TicketManagement.Application.Models.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Contracts.Infrastructure
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Email email);
    }
}
