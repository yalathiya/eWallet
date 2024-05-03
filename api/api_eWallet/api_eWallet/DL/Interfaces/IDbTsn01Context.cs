using api_eWallet.Models;
using api_eWallet.Models.POCO;

namespace api_eWallet.DL.Interfaces
{
    /// <summary>
    /// Interface for process transaction 
    /// </summary>
    public interface IDbTsn01Context
    {
        #region Public Methods

        /// <summary>
        /// Deposit money from bank account to wallet 
        /// </summary>
        /// <param name="objTsn01"> object of transaction </param>
        /// <returns> object of response </returns>
        Response Deposit(Tsn01 objTsn01);

        /// <summary>
        /// Transfer money from wallet to another wallet
        /// </summary>
        /// <param name="objTsn01"> object of transaction </param>
        /// <returns> object of response </returns>
        Response Transfer(Tsn01 objTsn01);

        /// <summary>
        /// Withdraw money from wallet to bank account 
        /// </summary>
        /// <param name="objTsn01"></param>
        /// <returns></returns>
        Response Withdraw(Tsn01 objTsn01);

        /// <summary>
        /// Get all transaction of wallet 
        /// </summary>
        /// <param name="walletId"> wallet id </param>
        /// <param name="pageNumber"> page number </param>
        /// <returns> list of transactions </returns>
        object GetAllTransactions(int walletId, int pageNumber);

        /// <summary>
        /// Get particular transaction
        /// </summary>
        /// <param name="walletId"> wallet id </param>
        /// <param name="transactionId"> transaction id </param>
        /// <returns> object consisting transaction details </returns>
        object GetTransaction(int walletId, int transactionId);

        /// <summary>
        /// Get Transactions for specific interval 
        /// </summary>
        /// <param name="start"> start time </param>
        /// <param name="end"> end time </param>
        /// <returns></returns>
        object GetTransactions(int walletId, DateTime start, DateTime end);

        #endregion
    }
}
