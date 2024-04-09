using api_eWallet.Common;
using Newtonsoft.Json;
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
        [JsonProperty("n01101")]
        public int n01f01 { get; set; }

        /// <summary>
        /// Wallet Id
        /// </summary>
        [JsonProperty("n01102")]
        public int n01f02 { get; set; }

        /// <summary>
        /// from user Id
        /// </summary>
        [JsonProperty("n01103")]
        public int n01f03 { get; set; }

        /// <summary>
        /// to user Id
        /// </summary>
        [JsonProperty("n01104")]
        public int n01f04 { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("n01105")]
        public double n01f05 { get; set; }

        /// <summary>
        /// Transaction Type
        /// </summary>
        [JsonProperty("n01106")]
        public TransactionType n01f06 { get; set; }

        /// <summary>
        /// Transaction Fee
        /// </summary>
        [JsonProperty("n01107")]
        public double n01f07 { get; set; }

        /// <summary>
        /// Description of Transaction
        /// </summary>
        [JsonProperty("n01108")]
        public string n01f08 { get; set; }

        /// <summary>
        /// Created on
        /// </summary>
        public DateTime n01f09 { get; set; }

        #endregion

    }
}
