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
        /// Process Razorpay Payment
        /// </summary>
        /// <param name="objDTORaz01"> object of dto model of razorpay </param>
        /// <param name="walletId"> wallet id </param>
        /// <returns> object of response </returns>
        Response ProcessPayment(DTORaz01 objDTORaz01, int walletId);

        #endregion
    }
}
