using api_eWallet.Services.Interfaces;
using api_eWallet.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace api_eWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLTest : ControllerBase
    {
        [HttpGet]
        [Route("TestConnection")]
        public IActionResult TestConnection()
        {
            using(MySqlConnection con = Static.DbConnection.CreateConnection())
            {
                con.Open();
            }
            return Ok("Connction established successfully");
        }

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
    }
}
