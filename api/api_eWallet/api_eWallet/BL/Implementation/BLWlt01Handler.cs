﻿using api_eWallet.BL.Interfaces;
using api_eWallet.Models;
using api_eWallet.Models.POCO;
using api_eWallet.Utilities;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace api_eWallet.BL.Implementation
{
    /// <summary>
    /// Implementation of IBLWalletHandler
    /// </summary>
    public class BLWlt01Handler : IBLWlt01Handler
    {
        #region Private Members

        /// <summary>
        /// OrmLite dbFactory
        /// </summary>
        private IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Response to action method
        /// </summary>
        private Response _objResponse;

        #endregion

        #region Constructor

        /// <summary>
        /// Configuring dependency injection
        /// </summary>
        /// <param name="dbFactory"> OrmLite database factory </param>
        /// <param name="notificationService"> notification service </param>
        public BLWlt01Handler(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;  
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get current balance of wallet
        /// </summary>
        /// <param name="walletId"> wallet id extracted from claims</param>
        /// <returns> object of response </returns>
        public Response GetCurrentBalance(int walletId)
        {
            _objResponse = new Response();

            using(var db = _dbFactory.Open())
            {
                double currentBalance = db.SingleById<Wlt01>(walletId).T01f03;

                var data = new { currentBalance};

                _objResponse.SetResponse("Fetched Current Balance", data);
                return _objResponse;
            }
        }

        #endregion
    }
}
