using api_eWallet.Models;

namespace api_eWallet.BL.Interfaces
{
    /// <summary>
    /// Interface for Wallet Controller
    /// </summary>
    public interface IBLWlt01Handler
    {
        #region Public Methods

        /// <summary>
        /// Get current balance of wallet
        /// </summary>
        /// <param name="walletId"> wallet id extracted from claims</param>
        /// <returns> object of response </returns>
        Response GetCurrentBalance(int walletId);

        #endregion

    }
}
