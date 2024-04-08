using api_eWallet.Common;

namespace api_eWallet.Models.POCO
{
    /// <summary>
    /// Class of POCO Model - Account
    /// </summary>
    public class Acc01
    {
        /// <summary>
        /// Account Number
        /// </summary>
        public int c01f01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public int c01f02 { get; set; }

        /// <summary>
        /// Current Balance
        /// </summary>
        public double c01f03 { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public Currency c01f05 { get; set; }

        /// <summary>
        /// Created On
        /// </summary>
        public DateTime c01f06 { get; set; }

        /// <summary>
        /// Updated on
        /// </summary>
        public int c01f07 { get; set; }
    }
}
