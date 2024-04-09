﻿using api_eWallet.Common;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

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
        [JsonProperty("n01f01")]
        public int n01101 { get; set; }

        /// <summary>
        /// Wallet Id
        /// </summary>
        [JsonProperty("n01f02")]
        public int n01102 { get; set; }

        /// <summary>
        /// from user id
        /// </summary>
        [JsonProperty("n01f03")]
        public int n01103 { get; set; }

        /// <summary>
        /// to user id
        /// </summary>
        [JsonProperty("n01f04")]
        public int n01104 { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("n01f05")]
        public double n01105 { get; set; }

        /// <summary>
        /// Transaction Type
        /// </summary>
        [JsonProperty("n01f06")]
        public TransactionType n01106 { get; set; }

        /// <summary>
        /// Transaction Fee
        /// </summary>
        [JsonProperty("n01f07")]
        public int n01107 { get; set; }

        /// <summary>
        /// Description of Transaction
        /// </summary>
        [JsonProperty("n01f08")]
        public int n01108 { get; set; }

        #endregion

    }
}
