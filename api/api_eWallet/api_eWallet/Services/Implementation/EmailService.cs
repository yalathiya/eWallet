using api_eWallet.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace api_eWallet.Services.Implementation
{
    /// <summary>
    /// Email Service which implements ISender
    /// </summary>
    public class EmailService : IEmailService
    {
        #region Private Members

        /// <summary>
        /// Api configuration from appSettings.json 
        /// </summary>
        private IConfiguration _config;

        #endregion

        #region Constructor

        /// <summary>
        /// Dependency of IConfiguration
        /// </summary>
        /// <param name="config"> interface implementation of IConfiguration </param>
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sends Email to user 
        /// </summary>
        /// <param name="email"> recipientEmail </param>
        /// <param name="message"> message </param>
        public void Send(string email, string message)
        {
            try
            {
                // sender details
                string senderEmail = _config["EmailSettings:SenderEmail"];
                string senderPassword = _config["EmailSettings:SenderPassword"];

                // fetch from jwt
                string recipientEmail = email;

                MailMessage mail = new MailMessage(senderEmail, recipientEmail);
                mail.Subject = "eWallet Notification";
                mail.Body = message;

                SmtpClient smtpClient = new SmtpClient(_config["EmailSettings:SmtpClient"]);
                smtpClient.Port = Convert.ToInt32(_config["EmailSettings:Port"]);
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

        #endregion

    }
}
