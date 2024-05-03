using api_eWallet.DL.Interfaces;
using api_eWallet.Models;
using api_eWallet.Models.POCO;
using api_eWallet.Services.Interfaces;
using api_eWallet.Utilities;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net;
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
        private readonly MySqlConnection _connection;

        /// <summary>
        /// Response to action method
        /// </summary>
        private Response _objResponse;

        /// <summary>
        /// logging support
        /// </summary>
        private readonly ILogging _logging;

        /// <summary>
        /// Notification Service 
        /// </summary>
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Model of notification 
        /// </summary>
        private readonly Not01 _objNot01;

        #endregion

        #region Constructor

        /// <summary>
        /// Dependency Injection 
        /// </summary>
        /// <param name="logging"> logging support </param>
        /// <param name="notificationService"> notification service </param>
        public DbTsn01Context(ILogging logging, 
                              INotificationService notificationService)
        {
            _logging = logging;
            _connection = new MySqlConnection(Utilities.DbConnection.GetConnectionString());
            _notificationService = notificationService;
            _objNot01 = new Not01();
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
            _objResponse = new Response();

            try
            {
                _connection.Open();
                _logging.LogTrace("Connection is opened for processing " + objTsn01);

                using (var transaction = _connection.BeginTransaction())
                {
                    _logging.LogTrace("Transaction is started : " + objTsn01);

                    try
                    {
                        // Lock the rows in the source and destination wallets
                        LockWalletRows(objTsn01.N01f03, 0, transaction);

                        // Add amount to destination wallet
                        AddAmountToWallet(objTsn01.N01f03, objTsn01.N01f10, transaction);

                        // Insert transaction record
                        InsertTransaction(objTsn01, transaction);

                        // Commit the transaction
                        transaction.Commit();

                        _logging.LogTrace("transaction is commited : " + objTsn01);

                        _objResponse.SetResponse("Deposit Successful", null);
                        return _objResponse;
                    }
                    catch (Exception ex)
                    {
                        // Roll back the transaction on exception
                        transaction.Rollback();

                        _logging.LogWarning("transaction is rollbacked : " + objTsn01);

                        _objNot01.SetNotification(objTsn01.N01f02, $"Deposit of {objTsn01.N01f10} is failed", true, false, DateTime.Now);
                        _notificationService.SendNotification(_objNot01);

                        _objResponse.SetResponse(true, HttpStatusCode.InternalServerError, "Deposit Failed : Rollback Executed", ex.Message);
                        return _objResponse;
                    }
                }
            }
            catch (Exception ex)
            {

                _logging.LogException(ex, ex.Message);

                _objNot01.SetNotification(objTsn01.N01f02, $"Deposit of {objTsn01.N01f10} is failed", true, false, DateTime.Now);
                _notificationService.SendNotification(_objNot01);

                _objResponse.SetResponse(true, HttpStatusCode.InternalServerError, "Deposit Failed", null);
                return _objResponse;
            }
            finally
            {
                _connection.Close();
                _logging.LogTrace("Connection is closed for processing " + objTsn01);
            }
        }

        /// <summary>
        /// Transfer money from wallet to another wallet
        /// </summary>
        /// <param name="objTsn01"> object of transaction </param>
        /// <returns> object of response </returns>
        public Response Transfer(Tsn01 objTsn01)
        {
            _objResponse = new Response();

            try
            {
                _connection.Open();
                _logging.LogTrace("Connection is opened for processing " + objTsn01);

                // Begin a database transaction
                using (var transaction = _connection.BeginTransaction())
                {
                    try
                    {
                        // Lock the rows in the source and destination wallets
                        LockWalletRows(objTsn01.N01f03, objTsn01.N01f04, transaction);

                        // Deduct amount from source wallet
                        DeductAmountFromWallet(objTsn01.N01f03, objTsn01.N01f10, transaction);

                        // Add amount to destination wallet
                        AddAmountToWallet(objTsn01.N01f04, objTsn01.N01f10, transaction);

                        // Insert transaction record
                        InsertTransaction(objTsn01, transaction);

                        // Commit the transaction
                        transaction.Commit();

                        _objResponse.SetResponse("Transaction Successful", null);
                        return _objResponse;
                    }
                    catch (Exception ex)
                    {
                        // Roll back the transaction on exception
                        transaction.Rollback();
                        _logging.LogWarning("transaction is rollbacked : " + objTsn01);

                        _objNot01.SetNotification(objTsn01.N01f02, $"Transfer of {objTsn01.N01f10} is failed", true, false, DateTime.Now);
                        _notificationService.SendNotification(_objNot01);

                        _objResponse.SetResponse(true, HttpStatusCode.InternalServerError, "Transfer Failed : Rollback Executed", ex.Message);
                        return _objResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                _logging.LogException(ex, ex.Message);

                _objNot01.SetNotification(objTsn01.N01f02, $"Transfer of {objTsn01.N01f10} is failed", true, false, DateTime.Now);
                _notificationService.SendNotification(_objNot01);

                _objResponse.SetResponse(true, HttpStatusCode.InternalServerError, "Transfer Failed", null);
                return _objResponse;
            }
            finally
            {
                _connection.Close();
                _logging.LogTrace("Connection is closed for processing " + objTsn01);
            }
        }

        /// <summary>
        /// Withdraw money from wallet to bank account 
        /// </summary>
        /// <param name="objTsn01"></param>
        /// <returns> object of response </returns>
        public Response Withdraw(Tsn01 objTsn01)
        {
             _objResponse = new Response();

            try
            {
                _connection.Open();
                _logging.LogTrace("Connection is opened for processing " + objTsn01);

                using (var transaction = _connection.BeginTransaction())
                {
                    try
                    {
                        // Lock the row in the wallet
                        LockWalletRows(objTsn01.N01f03, 0, transaction);

                        // Deduct amount from wallet
                        DeductAmountFromWallet(objTsn01.N01f03, objTsn01.N01f10, transaction);

                        // Insert transaction record
                        InsertTransaction(objTsn01, transaction);

                        // Commit the transaction
                        transaction.Commit();

                        _objResponse.SetResponse("Withdrawal Successful", null);
                        return _objResponse;
                    }
                    catch (Exception ex)
                    {
                        // Roll back the transaction on exception
                        transaction.Rollback();
                        _logging.LogWarning("transaction is rollbacked : " + objTsn01);

                        _objNot01.SetNotification(objTsn01.N01f02, $"Withdrawal of {objTsn01.N01f10} is failed", true, false, DateTime.Now);
                        _notificationService.SendNotification(_objNot01);

                        _objResponse.SetResponse(true, HttpStatusCode.InternalServerError, "Withdrawal Failed : Rollback Executed", ex.Message);
                        return _objResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                _objNot01.SetNotification(objTsn01.N01f02, $"Withdrawal of {objTsn01.N01f10} is failed", true, false, DateTime.Now);
                _notificationService.SendNotification(_objNot01);

                _logging.LogException(ex, ex.Message);
                _objResponse.SetResponse(true, HttpStatusCode.InternalServerError, "Withdrawal Failed", null);
                return _objResponse;
            }
            finally
            {
                _connection.Close();
                _logging.LogTrace("Connection is closed for processing " + objTsn01);
            }
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
            DataTable dtTransactions = new();

            // retrieve user from the database in form of DataTable
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Connection = _connection;
                command.CommandText = String.Format(@"SELECT 
                                                        N01F01 AS TRANSACTION_ID,
                                                        N01F05 AS AMOUNT,
                                                        CASE N01F06
                                                            WHEN 'D' THEN 'Deposit'
                                                            WHEN 'T' THEN 'Transfer'
                                                            WHEN 'W' THEN 'Withdrawal'
                                                        END AS TRANSACTION_TYPE,
                                                        N01F09 AS CREATED_ON
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
            return dtTransactions.ToList();
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
                                                        N01F05 AS AMOUNT,
                                                        CASE N01F06
                                                            WHEN 'D' THEN 'Deposit'
                                                            WHEN 'T' THEN 'Transfer'
                                                            WHEN 'W' THEN 'Withdrawal'
                                                        END AS TRANSACTION_TYPE,
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
            return dtTransaction.ToList();
        }

        /// <summary>
        /// Get Transactions for specific interval 
        /// </summary>
        /// <param name="start"> start time </param>
        /// <param name="end"> end time </param>
        /// <returns> data table of transactions </returns>
        public object GetTransactions(int walletId, DateTime start, DateTime end)
        {
            // Transaction details 
            DataTable dtTransactions = new();

            // retrieve user from the database in form of DataTable
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Connection = _connection;
                command.CommandText = String.Format(@"SELECT 
                                                        N01F01 AS ID,
                                                        N01F03 AS FROM_USER_ID,
                                                        N01F04 AS TO_USER_ID,
                                                        N01F05 AS AMOUNT,
                                                        CASE N01F06
                                                            WHEN 'D' THEN 'Deposit'
                                                            WHEN 'T' THEN 'Transfer'
                                                            WHEN 'W' THEN 'Withdrawal'
                                                        END AS TYPE,
                                                        N01F07 AS FEES,
                                                        N01F08 AS DESCRIPTION,
                                                        N01F09 AS DATE,
                                                        N01F10 AS TOTAL_AMOUNT
                                                     FROM
                                                        TSN01 AS TRANSACTION_DETAILS
                                                     WHERE
                                                        N01F02 = {0}"
                                                     , walletId);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dtTransactions);
            }
            return dtTransactions;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to acquire locks on the rows corresponding to the source and destination wallets
        /// </summary>
        /// <param name="n01f03"> from user id</param>
        /// <param name="n01f04"> to user id </param>
        /// <param name="transaction"> refer to mysql transaction </param>
        private void LockWalletRows(int n01f03, int n01f04, MySqlTransaction transaction)
        {
            // Execute SQL commands to acquire locks on the rows corresponding to the source and destination wallets
            // Acquired row level locks for UPDATE 
            using (var command = _connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = String.Format(@"SELECT 
                                                        T01F01,
                                                        T01F02,
                                                        T01F03,
                                                        T01F04,
                                                        T01F05,
                                                        T01F06
                                                      FROM 
                                                        WLT01 
                                                      WHERE 
                                                        T01F01 IN ({0}, {1}) 
                                                      FOR 
                                                        UPDATE", n01f03, n01f04);
                command.ExecuteNonQuery();
            }
            _logging.LogWarning($" database rows are locked for user id {n01f03} , {n01f04}");
        }

        /// <summary>
        /// Deduct amount from source wallet 
        /// </summary>
        /// <param name="n01f03"> source user id </param>
        /// <param name="n01f10"> total transaction amount </param>
        /// <param name="transaction"> refer to mysql transaction </param>
        private void DeductAmountFromWallet(int n01f03, double n01f10, MySqlTransaction transaction)
        {
            using (var command = _connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = String.Format(@"UPDATE 
                                                        WLT01 
                                                      SET 
                                                        T01F03 = T01F03 - {0},
                                                        T01F06 = '{1}'
                                                      WHERE 
                                                        T01F02 = {2}",
                                                      n01f10,
                                                      DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"),
                                                      n01f03);
                command.ExecuteNonQuery();
                _logging.LogWarning($"{n01f10} is deducted from user id {n01f03}");
            }

            _objNot01.SetNotification(n01f03, $"{n01f10} amount is deducted from your wallet", true, false, DateTime.Now);
            _notificationService.SendNotification(_objNot01);
        }

        /// <summary>
        /// Add amount from source wallet 
        /// </summary>
        /// <param name="n01f04"> destination user id </param>
        /// <param name="n01f10"> total transaction amount </param>
        /// <param name="transaction"> refer to mysql transaction </param>
        private void AddAmountToWallet(int n01f04, double n01f10, MySqlTransaction transaction)
        {
            using (var command = _connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = String.Format(@"UPDATE 
                                                        WLT01 
                                                      SET 
                                                        T01F03 = T01F03 + {0},
                                                        T01F06 = '{1}'
                                                      WHERE 
                                                        T01F02 = {2}",
                                                      n01f10,
                                                      DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"),
                                                      n01f04);
                command.ExecuteNonQuery();
                _logging.LogWarning($"{n01f10} is credited to user id {n01f04}");
            }

            _objNot01.SetNotification(n01f04, $"{n01f10} amount is added to your wallet", true, false, DateTime.Now);
            _notificationService.SendNotification(_objNot01);
        }

        /// <summary>
        /// Insert record in transaction table 
        /// </summary>
        /// <param name="objTsn01"> object of transaction </param>
        /// <param name="transaction"> refer to mysql transaction </param>
        private void InsertTransaction(Tsn01 objTsn01, MySqlTransaction transaction)
        {
            // Execute SQL command to insert a transaction record
            using (var command = _connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = String.Format(@"INSERT INTO 
                                                        TSN01 
                                                        (N01F01, N01F02, N01F03, N01F04, N01F05, N01F06, N01F07, N01F08, N01F09, N01F10) 
                                                      VALUES 
                                                        ({0}, {1}, {2}, 
                                                        CASE WHEN {3} = 0 
                                                              THEN NULL
                                                              ELSE {3} 
                                                        END,
                                                        {4}, '{5}', {6}, '{7}', '{8}', {9})",
                                                        objTsn01.N01f01,
                                                        objTsn01.N01f02,
                                                        objTsn01.N01f03,
                                                        objTsn01.N01f04,
                                                        objTsn01.N01f05,
                                                        objTsn01.N01f06.ToString(),
                                                        objTsn01.N01f07,
                                                        objTsn01.N01f08,
                                                        objTsn01.N01f09.ToString("yyyy-MM-dd HH-mm-ss"),
                                                        objTsn01.N01f10);
                command.ExecuteNonQuery();
                _logging.LogTrace($"Transaction is added {objTsn01}");
            }

        }

        #endregion
    }
}
