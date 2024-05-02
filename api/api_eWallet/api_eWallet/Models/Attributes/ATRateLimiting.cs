namespace api_eWallet.Models.Attributes
{
    /// <summary>
    /// Rate limiting attribute which defines time window & max requests 
    /// Will be applied on endpoints 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ATRateLimiting : Attribute
    {
        #region Public Members

        /// <summary>
        /// window-time (in seconds) within the maximum number of requests allowed
        /// </summary>
        public int TimeWindow { get; set; }

        /// <summary>
        /// Maximum requests per time window 
        /// </summary>
        public int MaxRequests { get; set; }

        #endregion
    }
}
