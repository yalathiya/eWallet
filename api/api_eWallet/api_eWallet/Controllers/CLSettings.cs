using api_eWallet.BL.Interfaces;
using api_eWallet.Models.Attributes;
using api_eWallet.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_eWallet.Controllers
{
    /// <summary>
    /// Controller for additional things in api
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLSettings : ControllerBase
    {
        #region Private Members

        /// <summary>
        /// BL for settings 
        /// </summary>
        private IBLSettingHandler _objBLSettings;

        #endregion

        #region Constructor
        
        /// <summary>
        /// Dependency injection over constructor
        /// </summary>
        /// <param name="objBLSettings"></param>
        public CLSettings(IBLSettingHandler objBLSettings)
        {
            _objBLSettings = objBLSettings;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Redirects to api documentation for help
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        [AllowAnonymous]
        [ATRateLimiting(MaxRequests = 5, TimeWindow = 2)]
        [Route("help")]
        public IActionResult Help()
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Help.pdf");
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/pdf", "Help.pdf");
        }

        /// <summary>
        /// To deactive users wallet 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("deactive")]
        [ATRateLimiting(MaxRequests = 1, TimeWindow = 60)]
        public IActionResult DeactiveWallet()
        {
            return Ok(_objBLSettings.DeactivateWallet(HttpContext.GetUserIdFromClaims()));
        }

        /// <summary>
        /// To active users wallet 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("active")]
        [ATRateLimiting(MaxRequests = 1, TimeWindow = 60)]
        public IActionResult ActiveWallet()
        {
            return Ok(_objBLSettings.ActivateWallet(HttpContext.GetUserIdFromClaims()));
        }

        #endregion
    }
}
