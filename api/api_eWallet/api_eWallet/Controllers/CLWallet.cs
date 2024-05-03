using api_eWallet.BL.Interfaces;
using api_eWallet.Models;
using api_eWallet.Models.Attributes;
using api_eWallet.Models.DTO;
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
        private readonly IBLWlt01Handler _objBLWlt01Handler;

        /// <summary>
        /// response to action methods
        /// </summary>
        private Response _objResponse;

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
        [ATRateLimiting(MaxRequests = 5, TimeWindow = 2)]
        [Route("balance")]
        public IActionResult GetCurrentBalance()
        {
            return Ok(_objBLWlt01Handler.GetCurrentBalance(HttpContext.GetWalletIdFromClaims()));
        }

        [HttpPost]
        [ATRateLimiting(MaxRequests = 1, TimeWindow = 20)]
        [Route("statement")]
        public IActionResult DownloadStatement([FromBody] DTOIvl01 objIvl01)
        {
            _objResponse = _objBLWlt01Handler.Validate(objIvl01);

            if(_objResponse.HasError)
            {
                return Ok(_objResponse);
            }

            byte[] fileBytes = _objBLWlt01Handler.GenerateFileBytes(objIvl01);

            return File(fileBytes, "application/pdf", "statement.pdf");
        }

        #endregion

    }
}
