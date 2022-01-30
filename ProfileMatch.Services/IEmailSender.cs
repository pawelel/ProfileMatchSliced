using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProfileMatch.Models.Entities;

namespace ProfileMatch.Services
{
    public interface IEmailSender
    {
        void SendEmail(ApplicationUser to, string subject, string body);
    }
}
