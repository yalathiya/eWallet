using api_eWallet.Common;
using ServiceStack.DataAnnotations;

namespace api_eWallet.Models.POCO
{
    /// <summary>
    /// POCO model of wallet class
    /// </summary>
    [Alias("Wlt01")]
    public class Wlt01
    {
        #region Public Members

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
        [IgnoreOnUpdate]
        public DateTime t01f05 { get; set; }

        /// <summary>
        /// Updated on
        /// </summary>
        [IgnoreOnInsert]
        public DateTime t01f06 { get; set; }

        #endregion

    }
}
