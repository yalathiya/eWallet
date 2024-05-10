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

        /// <summary>
        /// Logging Support 
        /// </summary>
        private ILogging _logging;

        /// <summary>
        /// Razorpay key 
        /// </summary>
        private string _key;

        /// <summary>
        /// Razorpay secret value 
        /// </summary>
        private string _secret;

        /// <summary>
        /// Razorpay Client
        /// </summary>
        private RazorpayClient _client;
        #endregion

        #region Constructor

        /// <summary>
        /// Dependency Injection over Constructor
        /// </summary>
        /// <param name="configuration"> app configurations </param>
        /// <param name="logging"> logging support </param>
        public RazorpayService(IConfiguration configuration,
                               ILogging logging)
        {
            _config = configuration;
            _logging = logging;
            _key = _config["Razorpay:Key"];
            _secret = _config["Razorpay:Secret"];
            _client = new RazorpayClient(_key, _secret);
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
            input.Add("amount", amount*100); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", walletId);

            Razorpay.Api.Order order = _client.Order.Create(input);
            string orderId = order["id"].ToString();

            _logging.LogTrace($"razorpay order is created {orderId} walletId : {walletId} amount : {amount}");
            
            return orderId;
        }

        /// <summary>
        /// Fetch payment by id 
        /// </summary>
        /// <param name="paymentId"> payment id </param>
        /// <returns> payment details </returns>
        public object FetchPaymentById(string paymentId)
        {
            Razorpay.Api.Payment payment = _client.Payment.Fetch(paymentId);

            _logging.LogTrace("Fetched Payment of " +  paymentId);  
            return payment;
        }

        #endregion
    }
}
