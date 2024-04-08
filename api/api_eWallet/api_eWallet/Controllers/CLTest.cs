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
    }
}
