using api_eWallet.Models.DTO;
using api_eWallet.Models;
using api_eWallet.Utilities;

namespace api_eWallet.BL.Interfaces
{
    /// <summary>
    /// Interface for all transactions 
    /// </summary>
    public interface IBLTsn01Handler
    {
        #region Public Members

        /// <summary>
        /// Type of transfer 
        /// D => Deposit
        /// T => Transfer
        /// W => Withdrawl
        /// </summary>
        EnmTransactionType EnmTransactionType { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Prevalidates DTO model
        /// </summary>
        /// <param name="objDTOTsn01"> DTO model of transaction </param>
        /// <param name="walletId"> wallet id extracted from claim </param>
        /// <returns> true => if validated successfully 
        ///           false => otherwise
        /// </returns>
        Response Prevalidation(DTOTsn01 objDTOTsn01, int walletId);

        /// <summary>
        /// Convert DTO model to POCO Model 
        /// </summary>
        /// <param name="objTsnUsr01"> DTO of Tsn01 </param>
        void Presave(DTOTsn01 objDTOTsn01);

        /// <summary>
        /// Validate POCO Model 
        /// </summary>
        /// <returns>true if validated else false </returns>
        Response Validate();

        /// <summary>
        /// Perform transaction. as per EnmOperation  
        /// </summary>
        Response Save();

        /// <summary>
        /// Get All Transaction Details        
        /// </summary>
        /// <param name="walletId"> wallet id extracted from claim </param>
        /// <param name="pageNumber"> page number </param>
        /// <returns> DTO model of user </returns>
        Response GetAllTransactions(int walletId,  int pageNumber);

        /// <summary>
        /// Get particular Transaction Details        
        /// </summary>
        /// <param name="walletId"> wallet id extracted from claim </param>
        /// <param name="transactionId"> transaction id </param>
        /// <returns> object of response </returns>
        Response GetTransaction(int walletId, int transactionId);

        #endregion
    }
}
