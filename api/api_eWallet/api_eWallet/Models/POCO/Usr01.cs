using Newtonsoft.Json;
using ServiceStack.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_eWallet.Models.POCO
{
    /// <summary>
    /// POCO model of user class
    /// </summary>
    [Alias("Usr01")]
    public class Usr01
    {
        #region Public Members

        /// <summary>
        /// user id
        /// </summary>
        public int r01f01 { get; set; }

        /// <summary>
        /// hash password
        /// </summary>
        [JsonProperty("r01102")]
        public string r01f03 { get; set; }

        /// <summary>
        /// email id
        /// </summary>
        [JsonProperty("r01101")]
        public string r01f04 { get; set; }

        /// <summary>
        /// first name
        /// </summary>
        [JsonProperty("r01103")]
        public string r01f05 { get; set; }

        /// <summary>
        /// last name        
        /// </summary>
        [JsonProperty("r01104")]
        public string r01f06 { get; set; }

        /// <summary>
        /// mobile number
        /// </summary>
        [JsonProperty("r01105")]
        public string r01f07 { get; set; }

        /// <summary>
        /// created on
        /// </summary>
        public DateTime r01f08 { get; set; }

        /// <summary>
        /// updated on
        /// </summary>
        public DateTime r01f09 { get; set; }

        /// <summary>
        /// isDeleted
        /// </summary>
        public bool r01f10 { get; set; }

        /// <summary>
        /// deleted on
        /// </summary>
        public DateTime r01f11 { get; set; }

        #endregion

    }
}
