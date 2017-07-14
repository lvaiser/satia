using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LoginPoC.Core.Messaging
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            MailAddress from = new MailAddress(
                ConfigurationManager.AppSettings["mailAccount"],
                "SATIA");

            // Set destinations for the e-mail message.
            MailAddress to = new MailAddress(message.Destination);

            var myMessage = new MailMessage(from, to);
            myMessage.Subject = message.Subject;
            myMessage.Body = message.Body;
            myMessage.IsBodyHtml = true;

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(
                    ConfigurationManager.AppSettings["mailAccount"],
                    ConfigurationManager.AppSettings["mailPassword"]),
                EnableSsl = true
            };

            return client.SendMailAsync(myMessage);
        }
    }
}
