using api_eWallet.Services.Interfaces;
using System.Net.Mail;
using System.Net;

namespace api_eWallet.Services.Implementation
{
    /// <summary>
    /// Email Service which implements ISender
    /// </summary>
    public class EmailService : ISender
    {
        /// <summary>
        /// Sends Email to user 
        /// </summary>
        /// <param name="recipientEmail"> recipientEmail </param>
        /// <param name="message"> message </param>
        public void Send(string message)
        {
            try
            {
                // sender details
                string senderEmail = "eWallet.notify.user@outlook.com";
                string senderPassword = "eWallet@7777";

                // fetch from jwt
                string recipientEmail = "";

                MailMessage mail = new MailMessage(senderEmail, recipientEmail);
                mail.Subject = "eWallet Notification";
                mail.Body = message;

                SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com");
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);

                Console.WriteLine("Registration email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending registration email: {ex.Message}");
            }
        }
    }
}
