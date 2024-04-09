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
        [JsonPropertyName("r01f04")]
        public string r01101 { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [JsonPropertyName("r01f03")]
        public string r01102 { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        [JsonPropertyName("r01f05")]
        public string r01103 { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [JsonPropertyName("r01f06")]
        public string r01104 { get; set; }

        /// <summary>
        /// Mobile Number
        /// </summary>
        [JsonPropertyName("r01f07")]
        public string r01105 { get; set; }

        #endregion

    }
}
