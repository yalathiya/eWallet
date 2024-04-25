using api_eWallet.DL.Interfaces;
using api_eWallet.Models;
using api_eWallet.Models.POCO;
using api_eWallet.Utilities;
using MySql.Data.MySqlClient;

namespace api_eWallet.DL.Implementation
{
    /// <summary>
    /// Implementation of IDbTsn01Context interface
    /// </summary>
    public class DbTsn01Context : IDbTsn01Context
    {
        #region Private Members

        /// <summary>
        /// MySql Connection  
        /// </summary>
        private MySqlConnection _connection;

        #endregion

        #region Constructor

        /// <summary>
        /// Configuring mysql connection
        /// </summary>
        public DbTsn01Context()
        {
            _connection = new MySqlConnection(DbConnection.GetConnectionString());
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Deposit money from bank account to wallet 
        /// </summary>
        /// <param name="objTsn01"> object of transaction </param>
        /// <returns> object of response </returns>
        public Response Deposit(Tsn01 objTsn01)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Transfer money from wallet to another wallet
        /// </summary>
        /// <param name="objTsn01"> object of transaction </param>
        /// <returns> object of response </returns>
        public Response Transfer(Tsn01 objTsn01)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Withdraw money from wallet to bank account 
        /// </summary>
        /// <param name="objTsn01"></param>
        /// <returns> object of response </returns>
        public Response Withdraw(Tsn01 objTsn01)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all transaction of wallet 
        /// </summary>
        /// <param name="walletId"> wallet id </param>
        /// <returns> list of transactions </returns>
        public object GetAllTransactions(int walletId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
