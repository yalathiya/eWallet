﻿using ServiceStack.DataAnnotations;

namespace api_eWallet.Models.POCO
{
    /// <summary>
    /// Class of POCO Model - Account
    /// </summary>
    [Alias("Acc01")]
    public class Acc01
    {
        #region Public Members 

        /// <summary>
        /// Account Number
        /// </summary>
        public int c01f01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public int c01f02 { get; set; }

        /// <summary>
        /// Current Balance
        /// </summary>
        public double c01f03 { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public string c01f05 { get; set; }

        /// <summary>
        /// Created On
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime c01f06 { get; set; }

        /// <summary>
        /// Updated on
        /// </summary>
        [IgnoreOnInsert]
        public int c01f07 { get; set; }

        #endregion

    }
}
