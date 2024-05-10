using api_eWallet.Models;
using api_eWallet.Models.DTO;

namespace api_eWallet.Services.Interfaces
{
    /// <summary>
    /// Interface of razorpay service 
    /// </summary>
    public interface IRazorpayService
    {
        #region Public Methods

        /// <summary>
        /// Generates order id 
        /// </summary>
        /// <param name="requestData"> contains necessary details for payments </param>
        /// <returns> order id  </returns>
        string GenerateOrderId(Dictionary<string, string> requestData);

        /// <summary>
        /// Fetch payment by id 
        /// </summary>
        /// <param name="paymentId"> payment id </param>
        /// <returns> payment details </returns>
        public object FetchPaymentById(string paymentId);

        #endregion
    }
}
