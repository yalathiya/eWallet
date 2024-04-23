using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace api_eWallet.Models.DTO
{
    /// <summary>
    /// Class of DTO model of login
    /// </summary>
    public class DTOLog01
    {
        #region Public Members 

        /// <summary>
        /// email Id
        /// </summary>
        [Required(ErrorMessage = "EmailId required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email id")]
        [JsonProperty("g01101")]
        public string G01f01 { get; set; }

        /// <summary>
        /// password
        /// </summary>
        [Required(ErrorMessage = "Password required")]
        [JsonProperty("g01102")]
        public string G01f02 { get; set; }

        #endregion

    }
}
