﻿using api_eWallet.BL.Interfaces;
using api_eWallet.DL.Interfaces;
using api_eWallet.Models;
using api_eWallet.Models.DTO;
using api_eWallet.Models.POCO;
using api_eWallet.Services.Interfaces;
using api_eWallet.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

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
        private readonly IBLWlt01Handler _objBLWlt01Handler;

        /// <summary>
        /// DbContext of Tsn01
        /// </summary>
        private readonly IDbTsn01Context _objDbTsn01Context;

        /// <summary>
        /// Implementation of logging
        /// </summary>
        private readonly ILogging _logging;

        /// <summary>
        /// Notification Service 
        /// </summary>
        private readonly INotificationService _notificationService;

        #endregion

        #region Constructor

        /// <summary>
        /// Configuring Dependency Injection
        /// </summary>
        /// <param name="logging"> logging support </param>
        /// <param name="objBLWlt01Handler"> BL for Wlt01 handler </param>
        /// <param name="objDbTsn01Context"> Bl for Tsn01 handler </param>
        /// <param name="notificationService"> Notification Service </param>
        public BLTsn01Handler(IBLWlt01Handler objBLWlt01Handler, 
                              IDbTsn01Context objDbTsn01Context,
                              ILogging logging,
                              INotificationService notificationService)
        {
            _logging = logging;
            _objBLWlt01Handler = objBLWlt01Handler;
            _objDbTsn01Context = objDbTsn01Context;
            _notificationService = notificationService;
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

            if (objDTOTsn01.N01f02 != walletId)
            {
                _objResponse.SetResponse(true, HttpStatusCode.Forbidden, "Request is forbidden due to access constraints", null);
                return _objResponse;
            }

            if (objDTOTsn01.N01f06 == EnmTransactionType.T.ToString())
            {
                EnmTransactionType = EnmTransactionType.T;
                if (objDTOTsn01.N01f04 == 0)
                {
                    _objResponse.SetResponse(true, HttpStatusCode.BadRequest, "Invalid to-user id", null);
                    return _objResponse;
                }
            }
            else if (objDTOTsn01.N01f06 == EnmTransactionType.W.ToString())
            {
                EnmTransactionType = EnmTransactionType.W;
            }
            else if (objDTOTsn01.N01f06 == EnmTransactionType.D.ToString())
            {
                EnmTransactionType = EnmTransactionType.D;
            }
            else
            {
                _objResponse.SetResponse(true, HttpStatusCode.BadRequest, "Invalid transaction type", null);
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

            _objTsn01.N01f06 = EnmTransactionType.ToString();
            _objTsn01.N01f09 = DateTime.Now;

            // Wallet to Wallet Transfer
            if(EnmTransactionType == EnmTransactionType.T)
            {
                _objTsn01.N01f10 = _objTsn01.N01f05;
            }
            // Deposit
            else if(EnmTransactionType == EnmTransactionType.D)
            {
                _objTsn01.N01f10 = _objTsn01.N01f05;
                _objTsn01.N01f04 = 0;
            }
            // Withdrawal
            else if(EnmTransactionType == EnmTransactionType.W)
            {
                _objTsn01.N01f10 = _objTsn01.N01f05 + _objTsn01.N01f07;
                _objTsn01.N01f04 = 0;
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
            Not01 objNot01 = new();

            // Wallet to Wallet Transfer
            if (EnmTransactionType == EnmTransactionType.T)
            {
                objNot01.SetNotification(_objTsn01.N01f02, "Your wallet transfer is processing", false, false, DateTime.Now);
                _notificationService.SendNotification(objNot01);

                return _objDbTsn01Context.Transfer(_objTsn01);
            }
            // Deposit
            else if (EnmTransactionType == EnmTransactionType.D)
            {
                objNot01.SetNotification(_objTsn01.N01f02, "Your deposit is processing", false, false, DateTime.Now);
                _notificationService.SendNotification(objNot01);

                return _objDbTsn01Context.Deposit(_objTsn01);
            }
            // Withdrawal
            else if (EnmTransactionType == EnmTransactionType.W)
            {
                objNot01.SetNotification(_objTsn01.N01f02, "Your withdrwal is processing", false, false, DateTime.Now);
                _notificationService.SendNotification(objNot01);

                return _objDbTsn01Context.Withdraw(_objTsn01);
            }

            _objResponse = new Response();
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

            _logging.LogTrace("Transactions Fetched from wallet-id : " + walletId);

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

            _logging.LogTrace("Transaction Fetched with the transaction-id " + transactionId);

            return _objResponse;
        }

        public override bool Equals(object? obj)
        {
            return obj is BLTsn01Handler handler &&
                   EqualityComparer<INotificationService>.Default.Equals(_notificationService, handler._notificationService);
        }

        #endregion
    }
}


