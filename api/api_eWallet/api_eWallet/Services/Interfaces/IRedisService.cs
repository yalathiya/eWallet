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

        /// <summary>
        /// Asynchronously get value from redis cache
        /// </summary>
        /// <typeparam name="T"> class of return type </typeparam>
        /// <param name="key"> key </param>
        /// <returns> object of T </returns>
        Task<T> GetCacheValueAsync<T>(string key);

        /// <summary>
        /// Asynchronously set key - value pair in redis cache 
        /// </summary>
        /// <typeparam name="T"> type of class </typeparam>
        /// <param name="key"> key </param>
        /// <param name="value"> object of T </param>
        /// <param name="expiry"> expiry time </param>
        /// <returns> response of task </returns>
        Task SetCacheValueAsync<T>(string key, T value, TimeSpan? expiry = null);

        #endregion
    }
}
