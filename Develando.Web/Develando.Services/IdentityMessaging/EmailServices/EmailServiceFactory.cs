using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Develando.Services.IdentityMessaging.EmailServices
{
    public class EmailServiceFactory : IEmailServiceFactory
    {
        private Dictionary<string, IIdentityMessageService> _servicies;

        public EmailServiceFactory()
        {
            _servicies = new Dictionary<string, IIdentityMessageService>();
        }

        public IIdentityMessageService CreateService(EmailCredentials credentials)
        {
            var email = credentials.EmailAddress.ToLower();

            if (!_servicies.ContainsKey(email))
            {
                IIdentityMessageService service;

                var emailType = GetEmailType(credentials.EmailAddress);
                switch (emailType)
                {
                    case EmailType.Gmail:
                        service = new GmailService(credentials);
                        break;
                    default:
                        throw new NotImplementedException($"{emailType}: there is no implemented instance for this type");
                }

                _servicies.Add(email, service);
            }

            return _servicies[email];
        }

        private EmailType GetEmailType(string emailAddress)
        {
            var email = emailAddress.ToLower();

            if (emailAddress.EndsWith("@gmail.com"))
                return EmailType.Gmail;

            if (emailAddress.EndsWith("@mail.ru"))
                return EmailType.MailRu;

            throw new ArgumentOutOfRangeException($"Unknown email type: {emailAddress}");
        }
    }
}
