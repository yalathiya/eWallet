using ServiceStack.DataAnnotations;

namespace api_eWallet.Models.POCO
{
    /// <summary>
    /// Class of POCO Model - Account
    /// </summary>
    [Alias("Acc01")]
    public class Acc01
    {
        #region Public Members 

        /// <summary>
        /// Account Number
        /// </summary>
        public int C01f01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public int C01f02 { get; set; }

        /// <summary>
        /// Current Balance
        /// </summary>
        public double C01f03 { get; set; }

        /// <summary>
        /// EnmCurrency
        /// </summary>
        public string C01f04 { get; set; }

        /// <summary>
        /// Created On
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime C01f05 { get; set; }

        /// <summary>
        /// Updated on
        /// </summary>
        [IgnoreOnInsert]
        public int C01f06 { get; set; }

        #endregion

    }
}
