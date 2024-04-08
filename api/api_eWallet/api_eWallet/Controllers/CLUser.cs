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

        /// <summary>
        /// Admin method which returns all user within application
        /// Also returns deleted users
        /// </summary>
        /// <returns> All user within database </returns>
        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Admin method which returns active users within application
        /// </summary>
        /// <returns> All users ( Not deleted ) </returns>
        [HttpGet]
        [Route("GetActiveUsers")]
        public IActionResult GetActiveUsers()
        {
            throw new NotImplementedException();
        }
    }
}
