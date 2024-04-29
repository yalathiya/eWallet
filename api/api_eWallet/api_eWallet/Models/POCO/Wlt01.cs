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
        [PrimaryKey]
        public int T01f01 { get; set; }

        /// <summary>
        /// user id
        /// </summary>
        public int T01f02 { get; set; }

        /// <summary>
        /// current balance
        /// </summary>
        public double T01f03 { get; set; }

        /// <summary>
        /// EnmCurrency
        /// </summary>
        public string T01f04 { get; set; } 

        /// <summary>
        /// Created on
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime T01f05 { get; set; }

        /// <summary>
        /// Updated on
        /// </summary>
        [IgnoreOnInsert]
        public DateTime T01f06 { get; set; }

        #endregion

    }
}
