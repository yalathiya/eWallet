using api_eWallet.BL.Interfaces;
using api_eWallet.Common;
using api_eWallet.Filters;
using api_eWallet.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace api_eWallet.Controllers
{
    /// <summary>
    /// consists method which deals with user data
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLUser : ControllerBase
    {
        #region Private Members

        private IBLUser _objBLUser;

        #endregion

        #region Public Methods

        /// <summary>
        /// Reference from DI
        /// </summary>
        /// <param name="objBLUser"></param>
        public CLUser(IBLUser objBLUser)
        {
            _objBLUser = objBLUser;
        }

        /// <summary>
        /// For User Registration
        /// </summary>
        /// <param name="DTOUsr"> DTO Model of User </param>
        /// <returns> jwt token through login method if user register successfully
        ///           else BadRequest
        /// </returns>
        [HttpPost]
        [Route("user")]
        public IActionResult Register([FromBody] DTOUsr01 objDTOUsr01)
        {
            // Presave
            _objBLUser.Presave(objDTOUsr01);

            // Validation
            bool isValidated = _objBLUser.Validate();

            if(!isValidated)
            {
                return BadRequest("Validation Unsuccessful");
            }

            _objBLUser.Save(Operation.Create);

            return Ok("User Registered");
        }

        /// <summary>
        /// Get user details
        /// </summary>
        /// <returns> User's Information </returns>
        [HttpGet]
        [Route("info")]
        [ServiceFilter(typeof(JwtAuthenticationFilter))]
        public IActionResult GetUserInfo()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update User Details 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        [ServiceFilter(typeof(JwtAuthenticationFilter))]
        public IActionResult UpdateUser()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
