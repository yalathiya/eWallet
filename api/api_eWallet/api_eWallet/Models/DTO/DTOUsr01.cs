using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace api_eWallet.Models.DTO
{
    /// <summary>
    /// DTO Model for user registration
    /// </summary>
    public class DTOUsr01
    {
        #region Public Members

        /// <summary>
        /// Email Id
        /// </summary>
        [JsonProperty("r01f04")]
        public string r01101 { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [JsonProperty("r01f03")]
        public string r01102 { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        [JsonProperty("r01f05")]
        public string r01103 { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [JsonProperty("r01f06")]
        public string r01104 { get; set; }

        /// <summary>
        /// Mobile Number
        /// </summary>
        [JsonProperty("r01f07")]
        public string r01105 { get; set; }

        #endregion

    }
}
