using api_eWallet.Models.DTO;
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
        /// <summary>
        /// to get current balance of user
        /// </summary>
        /// <returns> current balance </returns>
        [HttpGet]
        [Route("balance")]
        public IActionResult GetCurrentBalance()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deposit Money to Wallet from bank account 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("deposit")]
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
        public IActionResult Withdraw([FromBody] DTOTsn01 objDTOTsn01)
        {
            throw new NotImplementedException();
        }
    }
}
