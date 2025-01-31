﻿using ServiceStack.DataAnnotations;

namespace api_eWallet.Models.POCO
{
    /// <summary>
    /// Class of POCO model - Notification 
    /// </summary>
    [Alias("Not01")]
    public class Not01
    {
        #region Public Members

        /// <summary>
        /// Notification Id
        /// </summary>
        [PrimaryKey]
        public int T01f01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public int T01f02 { get; set; }

        /// <summary>
        /// Notification
        /// </summary>
        public string T01f03 { get; set; }

        /// <summary>
        /// Is Email Notification 
        /// </summary>
        public bool T01f04 { get; set; }

        /// <summary>
        /// Is SMS Notification 
        /// </summary>
        public bool T01f05 { get; set; }

        /// <summary>
        /// Created on        
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime T01f06 { get; set; }

        #endregion

    }
}
