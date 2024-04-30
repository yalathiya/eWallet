using api_eWallet.BL.Interfaces;
using api_eWallet.Filters;
using api_eWallet.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace api_eWallet.Controllers
{
    /// <summary>
    /// consists all methods which deals / extracting / updating data within wallet
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLWallet : ControllerBase
    {
        #region Private Members

        /// <summary>
        /// Reference of IBLWlt01 Handler
        /// </summary>
        private IBLWlt01Handler _objBLWlt01Handler;

        #endregion

        #region Constructor

        /// <summary>
        /// Dependency Injection
        /// </summary>
        /// <param name="objBLWlt01Handler"> BL class of Wlt01 Handler </param>
        public CLWallet(IBLWlt01Handler objBLWlt01Handler)
        {
            _objBLWlt01Handler = objBLWlt01Handler;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// to get current balance of user
        /// </summary>
        /// <returns> current balance </returns>
        [HttpGet]
        [Route("balance")]
        public IActionResult GetCurrentBalance()
        {
            return Ok(_objBLWlt01Handler.GetCurrentBalance(HttpContext.GetWalletIdFromClaims()));
        }

        #endregion

    }
}
