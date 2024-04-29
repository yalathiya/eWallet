using api_eWallet.BL.Interfaces;
using api_eWallet.DL.Interfaces;
using api_eWallet.Models;
using api_eWallet.Models.DTO;
using api_eWallet.Models.POCO;
using api_eWallet.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack;
using ServiceStack.Data;
using System.Data;
using System.Net;
using System.Transactions;

namespace api_eWallet.BL.Implementation
{
    /// <summary>
    /// Implementation of IBLTsn01Handler interface
    /// </summary>
    public class BLTsn01Handler : IBLTsn01Handler
    {
        #region Public Members

        /// <summary>
        /// Type of transfer 
        /// D => Deposit
        /// T => Transfer
        /// W => Withdrawl
        /// </summary>
        public EnmTransactionType EnmTransactionType { get; set; }

        #endregion

        #region Private Members

        /// <summary>
        /// Response to action method
        /// </summary>
        private Response _objResponse;

        /// <summary>
        /// POCO model of transsaction 
        /// </summary>
        private Tsn01 _objTsn01;

        /// <summary>
        /// Reference of Wallet related functionalities
        /// </summary>
        private IBLWlt01Handler _objBLWlt01Handler;

        /// <summary>
        /// DbContext of Tsn01
        /// </summary>
        private IDbTsn01Context _objDbTsn01Context;

        #endregion

        #region Constructor

        /// <summary>
        /// Configuring Dependency Injection
        /// </summary>
        public BLTsn01Handler(IBLWlt01Handler objBLWlt01Handler, IDbTsn01Context objDbTsn01Context)
        {
            _objBLWlt01Handler = objBLWlt01Handler;
            _objDbTsn01Context = objDbTsn01Context;
        }

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
        public Response Prevalidation(DTOTsn01 objDTOTsn01, int walletId)
        {
            _objResponse = new Response();

            if(objDTOTsn01.N01f02 != walletId)
            {
                _objResponse.SetResponse(true, HttpStatusCode.Forbidden, "Request is forbidden due to access constraints", null);
                return _objResponse;
            }

            _objResponse.SetResponse("prevalidation successful", null);
            return _objResponse;
        }

        /// <summary>
        /// Convert DTO model to POCO Model 
        /// </summary>
        /// <param name="objDTOTsn01"> DTO of Tsn01 </param>
        public void Presave(DTOTsn01 objDTOTsn01)
        {
            _objTsn01 = objDTOTsn01.ConvertModel<Tsn01>();

            _objTsn01.N01f09 = DateTime.Now;

            // Wallet to Wallet Transfer
            if(EnmTransactionType == EnmTransactionType.T)
            {
                _objTsn01.N01f06 = EnmTransactionType.ToString();
                _objTsn01.N01f10 = _objTsn01.N01f05;
            }
            // Deposit
            else if(EnmTransactionType == EnmTransactionType.D)
            {
                _objTsn01.N01f06 = EnmTransactionType.ToString();
                _objTsn01.N01f10 = _objTsn01.N01f05;
            }
            // Withdrawal
            else if(EnmTransactionType == EnmTransactionType.W)
            {
                _objTsn01.N01f06 = EnmTransactionType.ToString();
                _objTsn01.N01f10 = _objTsn01.N01f05 + _objTsn01.N01f07;
            }
        }

        /// <summary>
        /// Validate POCO Model 
        /// </summary>
        /// <returns>true if validated else false </returns>
        public Response Validate()
        {
            _objResponse = new Response();

            // Wallet to Wallet Transfer
            if (EnmTransactionType == EnmTransactionType.T)
            {

                if(_objTsn01.N01f07 != 0.0)
                {
                    _objResponse.SetResponse(true, HttpStatusCode.BadRequest, "Invalid Transaction Fees", null);
                    return _objResponse;
                }

                // Extracting current balance 
                var data = _objBLWlt01Handler.GetCurrentBalance(_objTsn01.N01f02).Data;
                string serializedData = JsonConvert.SerializeObject(data);
                JObject obj = JObject.Parse(serializedData);
                double currentBalance = (double)obj["currentBalance"];

                // has user sufficient balance
                if (_objTsn01.N01f10 > currentBalance)
                {
                    _objResponse.SetResponse(true, HttpStatusCode.Forbidden, "Insufficient balance to process the transaction", null);
                    return _objResponse;
                }
            }
            // Deposit
            else if (EnmTransactionType == EnmTransactionType.D)
            {
                if (_objTsn01.N01f07 != 0.0)
                {
                    _objResponse.SetResponse(true, HttpStatusCode.BadRequest, "Invalid Transaction Fees", null);
                    return _objResponse;
                }
            }
            // Withdrawal
            else if (EnmTransactionType == EnmTransactionType.W)
            {
                if (_objTsn01.N01f07 != (_objTsn01.N01f05*.02))
                {
                    _objResponse.SetResponse(true, HttpStatusCode.BadRequest, "Invalid Transaction Fees", null);
                    return _objResponse;
                }

                // Extracting current balance 
                var data = _objBLWlt01Handler.GetCurrentBalance(_objTsn01.N01f02).Data;
                string serializedData = JsonConvert.SerializeObject(data);
                JObject obj = JObject.Parse(serializedData);
                double currentBalance = (double)obj["currentBalance"];

                // has user sufficient balance
                if (_objTsn01.N01f10 > currentBalance)
                {
                    _objResponse.SetResponse(true, HttpStatusCode.Forbidden, "Insufficient balance to process the transaction", null);
                    return _objResponse;
                }
            }
            else
            {
                _objResponse.SetResponse(true, HttpStatusCode.Forbidden, "Invalid Transaction Type", null);
                return _objResponse;
            }

            _objResponse.SetResponse("Validation Successful", null);
            return _objResponse;
        }

        /// <summary>
        /// Perform transaction. as per EnmOperation  
        /// </summary>
        public Response Save()
        {
            _objResponse = new Response();

            // Wallet to Wallet Transfer
            if (EnmTransactionType == EnmTransactionType.T)
            {
                return _objDbTsn01Context.Transfer(_objTsn01);
            }
            // Deposit
            else if (EnmTransactionType == EnmTransactionType.D)
            {
             
            }
            // Withdrawal
            else if (EnmTransactionType == EnmTransactionType.W)
            {
             
            }

            _objResponse.SetResponse(true, HttpStatusCode.InternalServerError, "Failed to proceed transaction", null);
            return _objResponse;
        }

        /// <summary>
        /// Get All Transaction Details        
        /// </summary>
        /// <param name="walletId"> wallet id extracted from claim </param>
        /// <returns> object of response </returns>
        public Response GetAllTransactions(int walletId, int pageNumber)
        {
            _objResponse = new Response();
            _objResponse.SetResponse("Transactions Fetched", _objDbTsn01Context.GetAllTransactions(walletId, pageNumber));
            return _objResponse;
        }

        /// <summary>
        /// Get particular Transaction Details        
        /// </summary>
        /// <param name="walletId"> wallet id extracted from claim </param>
        /// <param name="transactionId"> transaction id </param>
        /// <returns> object of response </returns>
        public Response GetTransaction(int walletId, int transactionId)
        {
            _objResponse = new Response();
            _objResponse.SetResponse("Transaction Fetched", _objDbTsn01Context.GetTransaction(walletId, transactionId));
            return _objResponse;
        }

        #endregion
    }
}


