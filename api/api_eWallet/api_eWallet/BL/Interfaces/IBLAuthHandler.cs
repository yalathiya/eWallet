using api_eWallet.Models;

namespace api_eWallet.BL.Interfaces
{
    /// <summary>
    /// Interface for BL Authentication
    /// </summary>
    public interface IBLAuthHandler
    {
        #region Public Methods

        /// <summary>
        /// Login method which generates token for correct credential 
        /// </summary>
        /// <param name="email"> email id </param>
        /// <param name="password"> password </param>
        /// <returns> object of response </returns>
        Response Login(string email, string password);

        #endregion
    }
}
