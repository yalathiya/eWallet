﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace api_eWallet.Models.DTO
{
    /// <summary>
    /// DTO Model of transaction
    /// </summary>
    public class DTOTsn01
    {
        #region Public Members

        /// <summary>
        /// Transaction Id
        /// </summary>
        [JsonProperty("n01101")]
        public int N01f01 { get; set; }

        /// <summary>
        /// Wallet Id
        /// </summary>
        [Required(ErrorMessage = "wallet id required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Wallet id")]
        [JsonProperty("n01102")]
        public int N01f02 { get; set; }

        /// <summary>
        /// from user id
        /// </summary>
        [Required(ErrorMessage = "from-userid required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid from-user-id")]
        [JsonProperty("n01103")]
        public int N01f03 { get; set; }

        /// <summary>
        /// to user id
        /// </summary>
        [Required(ErrorMessage = "too-userid required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid to-userid")]
        [JsonProperty("n01104")]
        public int N01f04 { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [Required(ErrorMessage = "Amount Required")]
        [Range(1, double.MaxValue, ErrorMessage = "Invalid amount")]
        [JsonProperty("n01105")]
        public double N01f05 { get; set; }

        /// <summary>
        /// Transaction Fees
        /// </summary>
        [Required(ErrorMessage = "Transaction Fees Required")]
        [Range(0, double.MaxValue, ErrorMessage = "Invalid Transaction Fees")]
        [JsonProperty("n01107")]
        public double N01f07 { get; set; }

        /// <summary>
        /// Description of Transaction
        /// </summary>
        [JsonProperty("n01108")]
        public string N01f08 { get; set; }

        #endregion

    }
}
