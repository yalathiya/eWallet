using api_eWallet.Models.DTO;
using Microsoft.AspNetCore.Http;
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
            throw new NotImplementedException();
        }

    }
}
