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
        #region Public Methods

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get user details
        /// </summary>
        /// <returns> User's Information </returns>
        [HttpGet]
        [Route("info")]
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
        public IActionResult UpdateUser()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
