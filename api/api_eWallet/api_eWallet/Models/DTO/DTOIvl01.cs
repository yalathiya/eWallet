using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace api_eWallet.Models.DTO
{
    /// <summary>
    /// class for interval model 
    /// </summary>
    public class DTOIvl01
    {
        #region Public Members

        /// <summary>
        /// starting time of interval
        /// </summary>
        [JsonProperty("l01101")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "starting time of interval required")]
        public DateTime L01f01 { get; set; }

        /// <summary>
        /// ending time of interval 
        /// </summary>
        [JsonProperty("l01102")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "ending time of interval required")]
        public DateTime L01f02 { get; set; }

        #endregion
    }
}
