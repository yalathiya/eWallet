using api_eWallet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Razorpay.Api;

namespace api_eWallet.Controllers
{
    /// <summary>
    /// Tests controller which checks basic functionalities within function
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLTest : ControllerBase
    {
        #region Private Members

        /// <summary>
        /// api configurations
        /// </summary>
        private IConfiguration _config;

        #endregion

        #region Constructor

        /// <summary>
        /// Dependency injection
        /// </summary>
        public CLTest(IConfiguration configuration)
        {
            _config = configuration;
        }

        #endregion

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

        /// <summary>
        /// Tests Jwt authorization
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TestsJwtAuthorization")]
        public IActionResult TestsJwtAuthorization()
        {
            return Ok("Authorized");
        }

        /// <summary>
        /// Tests razorpay payment gateway 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("TestsRazorpay")]
        public IActionResult TestsRazorpay()
        {
            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", 50000); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", "12121");

            string key = _config["Razorpay:Key"];
            string secret = _config["Razorpay:Secret"];

            RazorpayClient client = new RazorpayClient(key, secret);

            Razorpay.Api.Order order = client.Order.Create(input);
            string orderId = order["id"].ToString();

            return Ok(orderId);
        }
        
        #endregion
    }
}
