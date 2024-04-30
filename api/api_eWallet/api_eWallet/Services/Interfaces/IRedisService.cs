namespace api_eWallet.Services.Interfaces
{
    /// <summary>
    /// Interface for Redis Caching Service 
    /// </summary>
    public interface IRedisService
    {
        #region Public Methods 

        /// <summary>
        /// Get by key from redis 
        /// </summary>
        /// <param name="key"> key </param>
        /// <returns> value </returns>
        string Get(string key);

        /// <summary>
        /// Set cahe in redis 
        /// </summary>
        /// <param name="key"> key </param>
        /// <param name="value"> value </param>
        /// <param name="expiry"> expiry of interval </param>
        void Set(string key, string value, TimeSpan expiry);

        #endregion
    }
}
