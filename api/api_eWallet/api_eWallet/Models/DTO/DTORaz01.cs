using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace api_eWallet.Models.DTO
{
    /// <summary>
    /// Razorpay model for successful payment 
    /// </summary>
    public class DTORaz01
    {
        #region Public Members

        /// <summary>
        /// Razorpay payment id
        /// </summary>
        [JsonProperty("razorpay_payment_id")]
        [Required(ErrorMessage = "payment id is required ")]
        public string razorpay_payment_id { get; set; }

        /// <summary>
        /// Razorpay order id 
        /// </summary>
        [JsonProperty("razorpay_order_id")]
        [Required(ErrorMessage = "order id is required ")]
        public string razorpay_order_id { get; set; }

        /// <summary>
        /// Razorpay signature 
        /// </summary>
        [JsonProperty("razorpay_signature")]
        [Required(ErrorMessage = "signature is required ")]
        public string razorpay_signature { get; set; }

        #endregion
    }
}
