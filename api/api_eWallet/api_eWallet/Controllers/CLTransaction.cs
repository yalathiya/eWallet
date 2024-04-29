using api_eWallet.BL.Interfaces;
using api_eWallet.Filters;
using api_eWallet.Models;
using api_eWallet.Models.DTO;
using api_eWallet.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace api_eWallet.Controllers
{
    /// <summary>
    /// Consists all endpoints which deal with transaction 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLTransaction : ControllerBase
    {
        #region Private Members

        /// <summary>
        /// BL logic of Transaction process
        /// </summary>
        private IBLTsn01Handler _objBLTsn01Handler;

        /// <summary>
        /// Response to action method
        /// </summary>
        private Response _objResponse;

        #endregion

        #region Constructor

        /// <summary>
        /// Adding Dependency Injection
        /// </summary>
        /// <param name="objBLTsn01Handler"> BL logic of transaction </param>
        public CLTransaction(IBLTsn01Handler objBLTsn01Handler)
        {
            _objBLTsn01Handler = objBLTsn01Handler;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Deposit Money to Wallet from bank account 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("deposit")]
        [ServiceFilter(typeof(JwtAuthenticationFilter))]
        public IActionResult Deposit([FromBody] DTOTsn01 objDTOTsn01)
        {
            _objBLTsn01Handler.EnmTransactionType = EnmTransactionType.D;

            // prevalidation
            _objResponse = _objBLTsn01Handler.Prevalidation(objDTOTsn01, HttpContext.GetWalletIdFromClaims());
            if (_objResponse.HasError)
            {
                return Ok(_objResponse);
            }

            // Presave
            _objBLTsn01Handler.Presave(objDTOTsn01);

            // validation
            _objResponse = _objBLTsn01Handler.Validate();
            if (_objResponse.HasError)
            {
                return Ok(_objResponse);
            }

            // save
            _objResponse = _objBLTsn01Handler.Save();

            return Ok(_objResponse);
        }

        /// <summary>
        /// Transfer Money from My Wallet to another Wallet  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("transfer")]
        [ServiceFilter(typeof(JwtAuthenticationFilter))]
        public IActionResult Transfer([FromBody] DTOTsn01 objDTOTsn01)
        {
            _objBLTsn01Handler.EnmTransactionType = EnmTransactionType.T;

            // prevalidation
            _objResponse = _objBLTsn01Handler.Prevalidation(objDTOTsn01, HttpContext.GetWalletIdFromClaims());
            if (_objResponse.HasError)
            {
                return Ok(_objResponse);
            }

            // Presave
            _objBLTsn01Handler.Presave(objDTOTsn01);

            // validation
            _objResponse = _objBLTsn01Handler.Validate();
            if (_objResponse.HasError)
            {
                return Ok(_objResponse);
            }

            // save
            _objResponse = _objBLTsn01Handler.Save();

            return Ok(_objResponse);
        }

        /// <summary>
        /// Withdraw money from wallet & transfer it into bank account   
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("withdraw")]
        [ServiceFilter(typeof(JwtAuthenticationFilter))]
        public IActionResult Withdraw([FromBody] DTOTsn01 objDTOTsn01)
        {
            _objBLTsn01Handler.EnmTransactionType = EnmTransactionType.W;

            // prevalidation
            _objResponse = _objBLTsn01Handler.Prevalidation(objDTOTsn01, HttpContext.GetWalletIdFromClaims());
            if (_objResponse.HasError)
            {
                return Ok(_objResponse);
            }

            // Presave
            _objBLTsn01Handler.Presave(objDTOTsn01);

            // validation
            _objResponse = _objBLTsn01Handler.Validate();
            if (_objResponse.HasError)
            {
                return Ok(_objResponse);
            }

            // save
            _objResponse = _objBLTsn01Handler.Save();

            return Ok(_objResponse);
        }

        /// <summary>
        /// Retrieve all transactions
        /// </summary>
        /// <returns>page of transaction</returns>
        [HttpPost]
        [Route("GetTransactions")]
        [ServiceFilter(typeof(JwtAuthenticationFilter))]
        public IActionResult GetAllTransaction(int pageNumber)
        {
            return Ok(_objBLTsn01Handler.GetAllTransactions(HttpContext.GetWalletIdFromClaims(), pageNumber));
        }

        /// <summary>
        /// Retrieve particular transaction
        /// </summary>
        /// <returns>transaction details</returns>
        [HttpPost]
        [Route("GetTransaction")]
        [ServiceFilter(typeof(JwtAuthenticationFilter))]
        public IActionResult GetTransaction(int transactionId)
        {
            return Ok(_objBLTsn01Handler.GetTransaction(HttpContext.GetWalletIdFromClaims(), transactionId));
        }

        #endregion
    }
}
