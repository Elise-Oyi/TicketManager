using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Models.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Infrastructure.Mail
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(Email email)
        {
            var mail = "lizken100@gmail.com";
            var pw = "KennedyLiz";

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            //var result = client.SendMailAsync
            //    (
            //      new MailMessage
            //      (
            //          from: mail,
            //          to: email.To,
            //          subject: email.Subject,
            //          message: email.Body

            //         )
            //      );

            //return result;
        }
    }
}
