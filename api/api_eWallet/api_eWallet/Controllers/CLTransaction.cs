using api_eWallet.Filters;
using api_eWallet.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace api_eWallet.Controllers
{
    /// <summary>
    /// Consists all endpoints which deal with transaction 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLTransaction : ControllerBase
    {
        #region Public Methods

        /// <summary>
        /// Retrieve all transaction
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("get")]
        [ServiceFilter(typeof(JwtAuthenticationFilter))]
        public IActionResult GetAllTransaction([FromBody] DTOTsn01 objDTOTsn01)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
