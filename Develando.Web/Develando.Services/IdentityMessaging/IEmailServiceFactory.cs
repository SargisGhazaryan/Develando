using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Develando.Services.IdentityMessaging
{
    interface IEmailServiceFactory
    {
        IIdentityMessageService CreateService(EmailCredentials credentials);
    }
}
