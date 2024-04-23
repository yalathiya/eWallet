using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace api_eWallet.Models.DTO
{
    /// <summary>
    /// DTO Model for user registration
    /// </summary>
    public class DTOUsr01
    {
        #region Public Members

        /// <summary>
        /// user id
        /// </summary>
        [Required(ErrorMessage = "userid required")]
        [Range(0, int.MaxValue, ErrorMessage = "incorrect format of userid")]
        [JsonProperty("r01101")]
        public int R01f01 { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "userid required")]
        [JsonProperty("r01102")]
        public string R01f03 { get; set; }

        /// <summary>
        /// Email Id
        /// </summary>
        [Required(ErrorMessage = "email-id required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "invalid email id")]
        [JsonProperty("r01103")]
        public string R01f04 { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        [Required(ErrorMessage = "first name required")]
        [JsonProperty("r01104")]
        public string R01f05 { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [Required(ErrorMessage = "last name required")]
        [JsonProperty("r01105")]
        public string R01f06 { get; set; }

        /// <summary>
        /// Mobile Number
        /// </summary>
        [Required(ErrorMessage = "mobile number required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "invalid mobile number")]
        [JsonProperty("r01106")]
        public string R01f07 { get; set; }

        #endregion

    }
}
