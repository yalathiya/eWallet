using api_eWallet.Models.DTO;
using api_eWallet.Services.Interfaces;
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
        /// Implemnts  IAuthentication interface
        /// </summary>
        private IAuthentication _auth;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor Injection
        /// </summary>
        /// <param name="authentication"> IAuthentication </param>
        public CLAuth(IAuthentication authentication)
        {
            _auth = authentication;
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
        [Route("")]
        public IActionResult Login([FromBody] DTOLog01 objDTOLog01)
        {
            if(!_auth.Login(objDTOLog01.g01101, objDTOLog01.g01102))
            {
                return BadRequest("Credential is incorrect");
            }

            var token = _auth.GenerateJwtToken(objDTOLog01.g01101, , 101);

            return Ok(token);
        }

        #endregion
    }
}
