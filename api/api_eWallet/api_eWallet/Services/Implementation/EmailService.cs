using api_eWallet.Services.Interfaces;

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
        /// <param name="message"> message </param>
        /// <exception cref="NotImplementedException"></exception>
        public void Send(string message)
        {
            throw new NotImplementedException();
        }
    }
}
