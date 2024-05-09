using api_eWallet.BL.Interfaces;
using api_eWallet.Models;
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
        [Route("Initiate")]
        public IActionResult InitiatePayment(double amount)
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
        //  when transaction is processed from server side then it will call this method to process it from server side 
        /// </summary>
        /// <returns> object of response </returns>
        [HttpPost]
        [Route("ProcessPayment")]
        public IActionResult ProcessPayment([FromBody] DTORaz01 objDTORaz01)
        {
            // update order record - Raz01
            _objBLRaz01Handler.EnmOperation = EnmOperation.U;

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

        #endregion

    }
}
