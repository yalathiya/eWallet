using api_eWallet.Services.Interfaces;
using StackExchange.Redis;

namespace api_eWallet.Services.Implementation
{
    /// <summary>
    /// Implementation of Redis Service interface 
    /// </summary>
    public class RedisService : IRedisService
    {
        #region Private Members

        /// <summary>
        /// Redis Connection
        /// </summary>
        private readonly ConnectionMultiplexer _redis;

        /// <summary>
        /// Redis database 
        /// </summary>
        private readonly IDatabase _db;

        /// <summary>
        /// refer to configuration setup of application
        /// </summary>
        private IConfiguration _config;

        #endregion

        #region Constructor

        /// <summary>
        /// Configures redis stack 
        /// </summary>
        /// <param name="config"> Refer to appSetting </param>
        public RedisService(IConfiguration config)
        {
            _config = config;
            _redis = ConnectionMultiplexer.Connect(String.Format(@"{0},password={1}", 
                                                                _config["RedisStack:Connectionstring"], 
                                                                _config["RedisStack:Password"]));
            _db = _redis.GetDatabase();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get value from cache
        /// </summary>
        /// <param name="key"> key </param>
        /// <returns> value </returns>
        public string Get(string key)
        {
            return _db.StringGet(key);
        }

        /// <summary>
        /// Set value in redis cache 
        /// </summary>
        /// <param name="key"> key </param>
        /// <param name="value"> value </param>
        /// <param name="expiry"> expiry duration </param>
        public void Set(string key, string value, TimeSpan expiry)
        {
            _db.StringSet(key, value, expiry);
        }

        #endregion
    }
}
