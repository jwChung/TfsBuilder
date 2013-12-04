using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    /// <summary>
    /// Represents an email logger.
    /// </summary>
    public class EmailLogger
    {
        /// <summary>
        /// Sends an email with the subject and the body.
        /// </summary>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The body of the email.</param>
        public virtual void Log(string subject, string body)
        {
            if (subject == null)
            {
                throw new ArgumentNullException("subject");
            }

            if (body == null)
            {
                throw new ArgumentNullException("body");
            }

            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                string gmailId = ConfigurationManager.AppSettings["GmailId"];
                string gmailPassword = ConfigurationManager.AppSettings["GmailPassword"];

                client.Credentials = new NetworkCredential(gmailId, gmailPassword);
                client.EnableSsl = true;

                client.Send(
                    @from: gmailId,
                    recipients: gmailId,
                    subject: subject,
                    body: body);
            }
        }
    }
}