namespace api_eWallet.Models
{
    /// <summary>
    /// Model for client statistics
    /// </summary>
    public class ClientStatistics
    {
        #region Public Members 

        /// <summary>
        /// Last successful response time for client key 
        /// </summary>
        public DateTime LastSuccessfulResponseTime { get; set; }

        /// <summary>
        /// Number of requests completed successfully calculated as per client key 
        /// </summary>
        public int NumberOfRequestsCompletedSuccessfully { get; set; }

        #endregion
    }
}
