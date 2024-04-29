using api_eWallet.Filters;
using api_eWallet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace api_eWallet.Controllers
{
    /// <summary>
    /// Tests controller which checks basic functionalities within function
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLTest : ControllerBase
    {
        #region Public Methods

        /// <summary>
        /// Tests MySwl Connection
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TestConnection")]
        public IActionResult TestConnection()
        {
            using (MySqlConnection con = new MySqlConnection(Utilities.DbConnection.GetConnectionString()))
            {
                con.Open();
            }
            return Ok("Connction established successfully");
        }

        /// <summary>
        /// Tests Cryptography
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TestCryptography")]
        public IActionResult TestCryptography([FromServices] IServiceProvider provider)
        {
            ICryptography _cryptography = provider.GetService<ICryptography>();

            string p = "yash";
            string cipher = _cryptography.Encrypt(p);
            string plain = _cryptography.Decrypt(cipher);

            return Ok(p + "  " + plain + "  "+ cipher);
        }

        [HttpGet]
        [Route("TestsJwtAuthorization")]
        [ServiceFilter(typeof(JwtAuthenticationFilter))]
        public IActionResult TestsJwtAuthorization()
        {
            return Ok("Authorized");
        }

        #endregion
    }
}
