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
        public int n01f01 { get; set; }

        /// <summary>
        /// Wallet Id
        /// </summary>
        public int n01f02 { get; set; }

        /// <summary>
        /// from user Id
        /// </summary>
        public int n01f03 { get; set; }

        /// <summary>
        /// to user Id
        /// </summary>
        public int n01f04 { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public double n01f05 { get; set; }

        /// <summary>
        /// Transaction Type
        /// </summary>
        public string n01f06 { get; set; }

        /// <summary>
        /// Transaction Fee
        /// </summary>
        public double n01f07 { get; set; }

        /// <summary>
        /// Description of Transaction
        /// </summary>
        public string n01f08 { get; set; }

        /// <summary>
        /// Created on
        /// </summary>
        public DateTime n01f09 { get; set; }

        #endregion

    }
}
