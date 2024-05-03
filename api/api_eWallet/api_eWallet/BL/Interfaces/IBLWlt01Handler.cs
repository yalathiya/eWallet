using api_eWallet.Models;
using api_eWallet.Models.DTO;

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

        /// <summary>
        /// Validate interval of statement 
        /// </summary>
        /// <param name="objDTOIvl"> object of interval </param>
        /// <returns> object of response </returns>
        Response Validate(DTOIvl01 objDTOIvl);

        /// <summary>
        /// Generate file bytes for statements
        /// </summary>
        /// <param name="objDTOIvl"> object of interval </param>
        /// <param name="walletId"> wallet id </param>
        /// <returns> byte array </returns>
        byte[] GenerateFileBytes(int walletId, DTOIvl01 objDTOIvl);
        
        #endregion

    }
}
