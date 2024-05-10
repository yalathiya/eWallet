using ServiceStack.DataAnnotations;

namespace api_eWallet.Models.POCO
{
    /// <summary>
    /// model of razorpay payments 
    /// </summary>
    [Alias("Raz01")]
    public class Raz01
    {
        #region Public Members

        /// <summary>
        /// Razorpay Order Id 
        /// </summary>
        [PrimaryKey]
        public string z01f01 { get; set; }

        /// <summary>
        /// Razorpay Payment Id
        /// </summary>
        public string z01f02 { get; set; }
            
        /// <summary>
        /// Razorpay signature
        /// </summary>
        public string z01f03 { get; set; }

        /// <summary>
        /// Amount 
        /// </summary>
        public double z01f04 { get; set; }

        /// <summary>
        /// Wallet Id 
        /// </summary>
        public int z01f05 { get; set; }

        /// <summary>
        /// Razorpay status
        /// </summary>
        public string z01f06 { get; set; }
        
        /// <summary>
        /// Is reflected in wallet 
        /// </summary>
        public bool z01f07 { get; set; }

        /// <summary>
        /// Created on 
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime z01f08 { get; set; }

        /// <summary>
        /// Updated on 
        /// </summary>
        [IgnoreOnInsert]
        public DateTime z01f09 { get; set; }

        #endregion
    }
}
