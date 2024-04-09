using api_eWallet.Common;
using ServiceStack.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("n01101")]
        public int n01f01 { get; set; }

        /// <summary>
        /// Wallet Id
        /// </summary>
        [JsonPropertyName("n01102")]
        public int n01f02 { get; set; }

        /// <summary>
        /// from user Id
        /// </summary>
        [JsonPropertyName("n01103")]
        public int n01f03 { get; set; }

        /// <summary>
        /// to user Id
        /// </summary>
        [JsonPropertyName("n01104")]
        public int n01f04 { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [JsonPropertyName("n01105")]
        public double n01f05 { get; set; }

        /// <summary>
        /// Transaction Type
        /// </summary>
        [JsonPropertyName("n01106")]
        public TransactionType n01f06 { get; set; }

        /// <summary>
        /// Transaction Fee
        /// </summary>
        [JsonPropertyName("n01107")]
        public double n01f07 { get; set; }

        /// <summary>
        /// Description of Transaction
        /// </summary>
        [JsonPropertyName("n01108")]
        public string n01f08 { get; set; }

        /// <summary>
        /// Created on
        /// </summary>
        public DateTime n01f09 { get; set; }

        #endregion

    }
}
