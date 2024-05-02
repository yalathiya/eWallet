using api_eWallet.Models;
using api_eWallet.Models.Attributes;
using api_eWallet.Services.Interfaces;
using System.Net;

namespace api_eWallet.Middlewares
{
    /// <summary>
    /// Consists implementation of rate limiting middleware 
    /// </summary>
    public class RateLimitingMiddleware
    {

        #region Private Members

        /// <summary>
        /// Delegate processing current request
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Redis Cache
        /// </summary>
        private IRedisService _redis;

        /// <summary>
        /// Logging Support 
        /// </summary>
        private ILogging _logging;

        #endregion

        #region Contsructor

        /// <summary>
        /// Dependency Injection over constructor 
        /// </summary>
        /// <param name="next"> request delegate </param>
        /// <param name="redisService"> redis caching </param>
        /// <param name="logging"> logging support </param>
        public RateLimitingMiddleware(RequestDelegate next, 
                                      IRedisService redisService,
                                      ILogging logging)
        {
            _next = next;
            _redis = redisService;
            _logging = logging;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// logic of rate limiting
        /// </summary>
        /// <param name="context"> context of request </param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var decorator = endpoint?.Metadata.GetMetadata<ATRateLimiting>();

            // No rate limiting is applied if no decorator is specified on endpoint 
            if (decorator is null)
            {
                _logging.LogTrace("No rate limiting is applied for this request ");
                await _next(context);
                return;
            }

            var key = GenerateClientKey(context);
            var clientStatistics = await GetClientStatisticsByKey(key);

            if (clientStatistics != null &&
                   (DateTime.Now < clientStatistics.LastSuccessfulResponseTime.AddSeconds(decorator.TimeWindow) &&
                   clientStatistics.NumberOfRequestsCompletedSuccessfully >= decorator.MaxRequests))
            {
                _logging.LogWarning($"Request is abondaned by rate limiting due to too many request with key {key}");
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                return;
            }

            await UpdateClientStatisticsStorage(key, decorator.MaxRequests);
            _logging.LogTrace("reuest is passed successfully from rate limiting middleware -- client key -- " + key);

            await _next(context);
        }

        #endregion

        #region Private Methods
        
        /// <summary>
        /// Generates client key based on ip address & request path 
        /// </summary>
        /// <param name="context"> http context </param>
        /// <returns> client key </returns>
        private static string GenerateClientKey(HttpContext context)
        {
            return $"{context.Request.Path}_{context.Connection.RemoteIpAddress}";
        }

        /// <summary>
        /// To get client statisics based on client key 
        /// </summary>
        /// <param name="key"> client key </param>
        /// <returns> object of ClientStatistics </returns>
        private async Task<ClientStatistics> GetClientStatisticsByKey(string key)
        {
            return await _redis.GetCacheValueAsync<ClientStatistics>(key);
        }

        /// <summary>
        /// update clien statistics in cache 
        /// </summary>
        /// <param name="key"> client key </param>
        /// <param name="maxRequests"> maximum requests </param>
        /// <returns> task response </returns>
        private async Task UpdateClientStatisticsStorage(string key, int maxRequests)
        {
            ClientStatistics objClientStatistics = await GetClientStatisticsByKey(key);

            if (objClientStatistics == null)
            {
                objClientStatistics = new ClientStatistics
                {
                    LastSuccessfulResponseTime = DateTime.Now,
                    NumberOfRequestsCompletedSuccessfully = 1
                };
            }
            else
            {
                objClientStatistics.LastSuccessfulResponseTime = DateTime.Now;
                objClientStatistics.NumberOfRequestsCompletedSuccessfully++;
            }

            await _redis.SetCacheValueAsync(key, objClientStatistics, TimeSpan.FromMinutes(1));

            _logging.LogTrace($"client statistics is updated for client key - {key} ");
        }

        #endregion
    }
}
