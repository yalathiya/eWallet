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
        public string razorpay_payment_id { get; set; }

        /// <summary>
        /// Razorpay order id 
        /// </summary>
        public string razorpay_order_id { get; set; }

        /// <summary>
        /// Razorpay signature 
        /// </summary>
        public string razorpay_signature { get; set; }

        #endregion
    }
}
