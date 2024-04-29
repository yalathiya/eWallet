using ServiceStack.DataAnnotations;

namespace api_eWallet.Models.POCO
{
    /// <summary>
    /// Class of POCO model - Transaciton
    /// </summary>
    [Alias("Tsn01")]
    public class Tsn01
    {
        #region Public Members

        /// <summary>
        /// Transaction Id
        /// </summary>
        [PrimaryKey]
        public int N01f01 { get; set; }

        /// <summary>
        /// Wallet Id
        /// </summary>
        public int N01f02 { get; set; }

        /// <summary>
        /// from user Id
        /// </summary>
        public int N01f03 { get; set; }

        /// <summary>
        /// to user Id
        /// </summary>
        public int N01f04 { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public double N01f05 { get; set; }

        /// <summary>
        /// Transaction Type
        /// </summary>
        public string N01f06 { get; set; }

        /// <summary>
        /// Transaction Fee
        /// </summary>
        public double N01f07 { get; set; }

        /// <summary>
        /// Description of Transaction
        /// </summary>
        public string N01f08 { get; set; }

        /// <summary>
        /// Created on
        /// </summary>
        public DateTime N01f09 { get; set; }

        /// <summary>
        /// Total amount of transaction
        /// </summary>
        public double N01f10 { get; set; }

        #endregion

    }
}
