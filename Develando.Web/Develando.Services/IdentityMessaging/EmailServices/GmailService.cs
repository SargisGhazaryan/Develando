using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Develando.Services.IdentityMessaging.EmailServices
{
    public class GmailService : IIdentityMessageService
    {
        private EmailCredentials _credentials;

        /// <summary>
        /// Sends Email messages via gmail client
        /// </summary>
        /// <param name="sender">sender, from email credentials</param>
        public GmailService(EmailCredentials credentials)
        {
            _credentials = credentials;
        }

        public Task SendAsync(IdentityMessage message)
        {
            return Task.Run(() =>
            {
                MailMessage mess = new System.Net.Mail.MailMessage();
                string fromEmail = _credentials.EmailAddress;
                string fromPW = _credentials.Password;
                string toEmail = message.Destination;
                mess.From = new MailAddress(fromEmail);
                mess.To.Add(toEmail);
                mess.Subject = message.Subject;
                mess.Body = message.Body;
                mess.IsBodyHtml = true;
                mess.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(fromEmail, fromPW);

                    smtpClient.Send(mess.From.ToString(), mess.To.ToString(),
                                    mess.Subject, mess.Body);
                }
            });
        }
    }
}
