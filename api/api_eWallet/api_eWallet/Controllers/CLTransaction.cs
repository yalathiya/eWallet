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
        /// Deposit Money to Wallet from bank account 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("deposit")]
        [ServiceFilter(typeof(JwtAuthenticationFilter))]
        public IActionResult Deposit([FromBody] DTOTsn01 objDTOTsn01)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Transfer Money from My Wallet to another Wallet  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("transfer")]
        [ServiceFilter(typeof(JwtAuthenticationFilter))]
        public IActionResult Transfer([FromBody] DTOTsn01 objDTOTsn01)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Withdraw money from wallet & transfer it into bank account   
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("withdraw")]
        [ServiceFilter(typeof(JwtAuthenticationFilter))]
        public IActionResult Withdraw([FromBody] DTOTsn01 objDTOTsn01)
        {
            throw new NotImplementedException();
        }

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
