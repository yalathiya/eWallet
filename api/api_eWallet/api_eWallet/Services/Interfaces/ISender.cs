namespace api_eWallet.Services.Interfaces
{
    /// <summary>
    /// Interface for any message sending 
    /// </summary>
    public interface ISender
    {
        #region Abstract Methods

        /// <summary>
        /// Sending message to user
        /// </summary>
        /// <param name="message"> message which we are sending to user </param>
        void Send(string message);

        #endregion

    }
}
