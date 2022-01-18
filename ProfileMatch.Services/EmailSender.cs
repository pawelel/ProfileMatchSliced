using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Runtime.Versioning;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Services
{
    public class EmailSender :IEmailSender
    {

        public  void SendEmail( ApplicationUser to, string subject, string body)
        {

          
            using var message = new MimeMessage();
            message.Subject = subject;


            message.From.Add(new MailboxAddress("Profile Match Team", "info@profilematch.pl"));
            message.To.Add(MailboxAddress.Parse(to.Email));
            message.Body = new TextPart("html", body);

            SmtpClient client = new();
            try
            {
                client.Connect("profilematch.pl", 587, MailKit.Security.SecureSocketOptions.SslOnConnect);
                client.Authenticate("info@DEFAULT", "PMInfoSecret123$");
                client.Send(message);
                Console.WriteLine("Email Sent!");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
            
        }
    
        
    }
}
