using ServiceStack.DataAnnotations;

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
        public string r01f03 { get; set; }

        /// <summary>
        /// email id
        /// </summary>
        public string r01f04 { get; set; }

        /// <summary>
        /// first name
        /// </summary>
        public string r01f05 { get; set; }

        /// <summary>
        /// last name        
        /// </summary>
        public string r01f06 { get; set; }

        /// <summary>
        /// mobile number
        /// </summary>
        public string r01f07 { get; set; }

        /// <summary>
        /// created on
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime r01f08 { get; set; }

        /// <summary>
        /// updated on
        /// </summary>
        [IgnoreOnInsert]
        public DateTime r01f09 { get; set; }

        #endregion

    }
}
