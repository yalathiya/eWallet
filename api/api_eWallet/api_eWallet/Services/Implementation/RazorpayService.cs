using api_eWallet.Models.DTO;
using api_eWallet.Models;
using api_eWallet.Services.Interfaces;
using Razorpay.Api;

namespace api_eWallet.Services.Implementation
{
    /// <summary>
    /// Implements razorpay Service 
    /// </summary>
    public class RazorpayService : IRazorpayService
    {
        #region Private Members

        /// <summary>
        /// app configurations
        /// </summary>
        private IConfiguration _config;

        #endregion

        #region Constructor

        /// <summary>
        /// Dependency Injection over Constructor
        /// </summary>
        /// <param name="config"> app configurations </param>
        public RazorpayService(IConfiguration configuration)
        {
            _config = configuration;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Generates order id 
        /// </summary>
        /// <param name="requestData"> contains necessary details for payments </param>
        /// <returns> order id  </returns>
        public string GenerateOrderId(Dictionary<string, string> requestData)
        {
            double amount = Convert.ToDouble(requestData["amount"]);
            string walletId = requestData["walletId"];

            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", amount); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", walletId);

            string key = _config["Razorpay:Key"];
            string secret = _config["Razorpay:Secret"];

            RazorpayClient client = new RazorpayClient(key, secret);

            Razorpay.Api.Order order = client.Order.Create(input);
            string orderId = order["id"].ToString();

            return orderId;
        }

        /// <summary>
        /// Process Razorpay Payment
        /// </summary>
        /// <param name="objDTORaz01"> object of dto model of razorpay </param>
        /// <param name="walletId"> wallet id </param>
        /// <returns></returns>
        public Response ProcessPayment(DTORaz01 objDTORaz01, int walletId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
