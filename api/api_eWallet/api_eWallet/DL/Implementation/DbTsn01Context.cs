using api_eWallet.DL.Interfaces;
using api_eWallet.Models;
using api_eWallet.Models.POCO;
using api_eWallet.Utilities;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Transactions;

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

        /// <summary>
        /// Response to action method
        /// </summary>
        private Response _objResponse;

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
        /// <param name="pageNumber"> page number </param>
        /// <returns> list of transactions </returns>
        public object GetAllTransactions(int walletId, int pageNumber)
        {
            // Transaction details 
            DataTable dtTransactions = new DataTable();

            // retrieve user from the database in form of DataTable
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Connection = _connection;
                command.CommandText = String.Format(@"SELECT 
                                                        N01F01 AS TRANSACTION_ID,
                                                        N01F05 AS AMOUNT
                                                        N01F06 AS TRANSACTION_TYPE,
                                                        N01F09 AS CREATED_ON,
                                                     FROM
                                                        TSN01 AS TRANSACTION_DETAILS
                                                     WHERE
                                                        N01F02 = {0}
                                                     LIMIT 
                                                        10
                                                     OFFSET
                                                        {1}"
                                                     , walletId, pageNumber*10);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dtTransactions);
            }
            return JsonConvert.SerializeObject(dtTransactions);
        }

        /// <summary>
        /// Get particular transaction
        /// </summary>
        /// <param name="walletId"> wallet id </param>
        /// <param name="transactionId"> transaction id </param>
        /// <returns> object consisting transaction details </returns>
        public object GetTransaction(int walletId, int transactionId)
        {
            // Transaction details 
            DataTable dtTransaction = new DataTable();

            // retrieve user from the database in form of DataTable
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Connection = _connection;
                command.CommandText = String.Format(@"SELECT 
                                                        N01F01 AS TRANSACTION_ID,
                                                        N01F02 AS WALLET_ID,
                                                        N01F03 AS FROM_USER_ID,
                                                        N01F04 AS TO_USER_ID,
                                                        N01F05 AS AMOUNT
                                                        N01F06 AS TRANSACTION_TYPE,
                                                        N01F07 AS TRANSACTION_FEES,
                                                        N01F08 AS DESCRIPTION,
                                                        N01F09 AS CREATED_ON,
                                                        N01F10 AS TOTAL_AMOUNT
                                                     FROM
                                                        TSN01 AS TRANSACTION_DETAILS
                                                     WHERE
                                                        N01F01 = {0} AND N01F02 = {1}"
                                                     , transactionId, walletId);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dtTransaction);
            }
            return JsonConvert.SerializeObject(dtTransaction);
        }

        #endregion
    }
}
