using api_eWallet.Models;
using api_eWallet.Models.DTO;
using api_eWallet.Services.Interfaces;
using api_eWallet.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
        /// generates order id 
        /// </summary>
        /// <returns> order id </returns>
        [HttpPost]
        [Route("GenerateOrderId")]
        public IActionResult GenerateOrderId([FromBody] double amount)
        {
            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", amount); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", HttpContext.GetUserIdFromClaims().ToString());

            string key = _config["Razorpay:Key"];
            string secret = _config["Razorpay:Secret"];

            RazorpayClient client = new RazorpayClient(key, secret);

            Razorpay.Api.Order order = client.Order.Create(input);
            string orderId = order["id"].ToString();

            Response objResponse = new Response();
            objResponse.SetResponse(orderId, null);
            return Ok(objResponse);
        }

        /// <summary>
        /// Processes razorpay payment 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PaymentByRazorpay")]
        public IActionResult PaymentByRazorpay([FromBody] DTORaz01 objDTORaz01)
        {
            //// authorization header
            //_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(_config["Razorpay:Key"] + ":" + _config["Razorpay:Secret"])));

            //// Make the GET request synchronously
            //var response = _httpClient.GetAsync($"https://api.razorpay.com/v1/payments/{objDTORaz01.razorpay_payment_id}").Result;

            //// Check if the request was successful
            //if (response.IsSuccessStatusCode)
            //{
            //    // Read the response content synchronously
            //    var content = response.Content.ReadAsStringAsync().Result;

            //    // Deserialize the content into PaymentDetails object
            //    //var paymentDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<PaymentDetails>(content);

            //    return Ok(content);
            //}
            //else
            //{
            //    // Handle unsuccessful response
            //    throw new Exception($"Failed to fetch payment details. Status code: {response.StatusCode}");
            //}
            //return Ok(objDTORaz01.razorpay_payment_id);
            return Ok();
        }
        #endregion
    }
}
