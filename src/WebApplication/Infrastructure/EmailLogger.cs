using System;
using System.Net;
using System.Net.Mail;

namespace Jwc.TfsBuilder.WebApplication.Infrastructure
{
    /// <summary>
    /// Represents an email logger.
    /// </summary>
    public class EmailLogger : ILogger
    {
        /// <summary>
        /// Sends an email with the <paramref name="subject"/> and the <paramref name="body"/>.
        /// </summary>
        /// <param name="subject">A subject of the email.</param>
        /// <param name="body">A body of the email.</param>
        public void Log(string subject, string body)
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
                string gmailId = AppSettings.Instance.GmailId;
                string gmailPassword = AppSettings.Instance.GmailPassword;

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