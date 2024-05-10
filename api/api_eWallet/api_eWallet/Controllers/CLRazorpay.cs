using api_eWallet.BL.Interfaces;
using api_eWallet.Models;
using api_eWallet.Models.Attributes;
using api_eWallet.Models.DTO;
using api_eWallet.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace api_eWallet.Controllers
{
    /// <summary>
    /// Handling Razorpay Payments 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLRazorpay : ControllerBase
    {
        #region Private Members

        /// <summary>
        /// refer to IBLRaz01 handler
        /// BL of Razorpay transaction
        /// </summary>
        private IBLRaz01Handler _objBLRaz01Handler;

        /// <summary>
        /// Response to action method
        /// </summary>
        private Response _objResponse;

        #endregion

        #region Constructor

        /// <summary>
        /// Dependency injection over constructor
        /// </summary>
        /// <param name="objBLRaz01Handler"> BL of Raz01 </param>
        public CLRazorpay(IBLRaz01Handler objBLRaz01Handler)
        {
            _objBLRaz01Handler = objBLRaz01Handler;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initiate razorpay payment  
        /// </summary>
        /// <returns> object of response consisting order id </returns>
        [HttpPost]
        [ATRateLimiting(MaxRequests = 2, TimeWindow = 5)]
        [Route("Initiate")]
        public IActionResult InitiatePayment([FromForm] double amount)
        {
            // create order record in database - Raz01
            _objBLRaz01Handler.EnmOperation = EnmOperation.C;

            // presave
            _objBLRaz01Handler.Presave(amount, HttpContext.GetWalletIdFromClaims());

            // validate
            _objResponse = _objBLRaz01Handler.Validate();
            if(_objResponse.HasError)
            {
                return Ok(_objResponse);
            }

            // save
            _objResponse = _objBLRaz01Handler.Save();

            return Ok(_objResponse);
        }

        /// <summary>
        /// Processes payment
        /// processes razorpay payment at server side after client side execution 
        /// </summary>
        /// <returns> object of response </returns>
        [HttpPost]
        [ATRateLimiting(MaxRequests = 2, TimeWindow = 5)]
        [Route("ProcessPayment")]
        public IActionResult ProcessPayment([FromBody] DTORaz01 objDTORaz01)
        {
            _objBLRaz01Handler.EnmOperation = EnmOperation.U;
            _objBLRaz01Handler.UserId = HttpContext.GetUserIdFromClaims();
            _objBLRaz01Handler.WalletId = HttpContext.GetWalletIdFromClaims();

            // Prevalidation
            _objResponse = _objBLRaz01Handler.Prevalidation(objDTORaz01);
            if(_objResponse.HasError)
            {
                return Ok(_objResponse);
            }

            // Presave
            _objBLRaz01Handler.Presave(objDTORaz01);

            // Validation
            _objResponse = _objBLRaz01Handler.Validate();
            if(_objResponse.HasError)
            {
                return Ok(_objResponse);
            }

            // Save
            _objResponse = _objBLRaz01Handler.Save();

            return Ok(_objResponse);
        }

        /// <summary>
        /// Fetch Payment by payment id 
        /// </summary>
        /// <param name="id"> payment id </param>
        /// <returns> object of response </returns>
        [HttpGet]
        [ATRateLimiting(MaxRequests = 2, TimeWindow = 1)]
        [Route("Fetch")]
        public IActionResult FetchPayment(string id)
        {
            _objResponse = _objBLRaz01Handler.FetchPayment(id);
            return Ok(_objResponse);
        }
        #endregion

    }
}
