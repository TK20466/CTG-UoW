using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CtgBusinessLogic.Utilities
{
    public static class MailUtilities
    {
        public static void SendEmail(string email, Guid verificationCode, string path)
        {
            
            MailMessage mail = new MailMessage();
            mail.Subject = "CTG Event Tracker - Verify your email";
            mail.Body = "Welcome to the CTG Event Tracker!  Please visit this url to verify your email and log in:  " +
                        path + verificationCode;
            mail.To.Add(new MailAddress(email));
            SendMail(mail);
        }

        private static void SendMail(MailMessage message)
        {
            var url = ConfigurationManager.AppSettings["smtp-url"];
            var port = int.Parse(ConfigurationManager.AppSettings["smtp-port"]);
            var source = ConfigurationManager.AppSettings["smtp-source"];
            var password = ConfigurationManager.AppSettings["smtp-password"];

            using (var smtpClient = new SmtpClient(url, port)
                {
                    Credentials = new NetworkCredential(source, password),
                    EnableSsl = true
                })
            {
                //smtpClient.UseDefaultCredentials = false;
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                message.From = new MailAddress(source, "CTG Event Manager");

                smtpClient.Send(message);
            }
        }
    }
}
