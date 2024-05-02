using api_eWallet.BL.Interfaces;
using api_eWallet.Models.Attributes;
using api_eWallet.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_eWallet.Controllers
{
    /// <summary>
    /// Authentication Controller 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLAuth : ControllerBase
    {
        #region Private Members

        /// <summary>
        /// Implemnts  IBLAuthHandler interface
        /// </summary>
        private IBLAuthHandler _objBLAuthHandler;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor Injection
        /// </summary>
        /// <param name="authentication"> IAuthentication </param>
        public CLAuth(IBLAuthHandler objBLAuthHandler)
        {
            _objBLAuthHandler = objBLAuthHandler;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Authenticates user credential
        /// </summary>
        /// <param name="objDTOLog01"> Login Model </param>
        /// <returns> jwt token => if credential is correct
        ///           else => Login Failed 
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ATRateLimiting(MaxRequests = 1, TimeWindow = 2)]
        [Route("")]
        public IActionResult Login([FromBody] DTOLog01 objDTOLog01)
        {
            return Ok(_objBLAuthHandler.Login(objDTOLog01.G01f01, objDTOLog01.G01f02));
        }

        #endregion
    }
}
