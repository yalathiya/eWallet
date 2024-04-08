using api_eWallet.Common;

namespace api_eWallet.Models.POCO
{
    /// <summary>
    /// POCO model of wallet class
    /// </summary>
    public class Wlt01
    {
        /// <summary>
        /// Wallet id
        /// </summary>
        public int t01f01 { get; set; }

        /// <summary>
        /// user id
        /// </summary>
        public int t01f02 { get; set; }

        /// <summary>
        /// current balance
        /// </summary>
        public double t01f03 { get; set; }

        /// <summary>
        /// currency
        /// </summary>
        public Currency t01f04 { get; set; }

        /// <summary>
        /// Created on
        /// </summary>
        public DateTime t01f05 { get; set; }

        /// <summary>
        /// Updated on
        /// </summary>
        public DateTime t01f06 { get; set; }
    }
}
