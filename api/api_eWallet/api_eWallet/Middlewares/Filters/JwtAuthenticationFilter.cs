using api_eWallet.Services.Interfaces;
using api_eWallet.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace api_eWallet.Middlewares.Filters
{
    /// <summary>
    /// Authentication filter which valides jwt token
    /// </summary>
    public class JwtAuthenticationFilter : IAuthorizationFilter
    {
        #region Private Members

        /// <summary>
        /// Implements IAuthentication interface
        /// </summary>
        private readonly IAuthentication _authService;

        /// <summary>
        /// Redis Stack Service 
        /// </summary>
        private readonly IRedisService _redisService;

        /// <summary>
        /// Expiry time of cache
        /// </summary>
        private readonly TimeSpan _expiryTime = TimeSpan.FromMinutes(20);

        #endregion

        #region Constructor

        /// <summary>
        /// Reference of IoC
        /// </summary>
        /// <param name="authService"> IAuthentication </param>
        /// <param name="redisService"> IRedisService </param>
        public JwtAuthenticationFilter(IAuthentication authService, IRedisService redisService)
        {
            _authService = authService;
            _redisService = redisService;
        }

        #endregion

        #region OnAuthorization

        /// <summary>
        /// Core Logic of Authorization Filters
        /// It's role based authorization
        /// </summary>
        /// <param name="context"> AuthorizationFilter Context </param>
        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
        {
            // Retrieve the endpoint information
            var endpoint = context.HttpContext.GetEndpoint();

            // Check if the endpoint is excluded from authentication
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return; // Skip authentication for this endpoint
            }

            // Retrieve authorization header from request
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"];

            // Check if authorization header is present and has the Bearer scheme
            if (authorizationHeader.Count == 1 && authorizationHeader.FirstOrDefault().StartsWith("Bearer "))
            {
                var token = authorizationHeader.FirstOrDefault()["Bearer ".Length..];

                // check token present in cache
                string cachedValue = _redisService.Get(token);
                if (!string.IsNullOrWhiteSpace(cachedValue))
                {
                    string[] cachedData = cachedValue.Split(" ");

                    var claims = new List<Claim>
                                     {
                                        new Claim("jwt_emailId", cachedData[2]),
                                        new Claim("jwt_userId", Convert.ToString(cachedData[0])),
                                        new Claim("jwt_walletId", Convert.ToString(cachedData[1])),
                                     };

                    var identity = new ClaimsIdentity(claims, "jwt");
                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    // Set the ClaimsPrincipal in HttpContext.User
                    context.HttpContext.User = claimsPrincipal;

                    // extend expiry time
                    _redisService.Set(token, cachedValue, _expiryTime);

                    return;
                }
                try
                {
                    // Validate the JWT token
                    var claimsPrincipal = _authService.ValidateToken(token);

                    if (claimsPrincipal != null)
                    {
                        // Set the User property on the context with the validated claims
                        context.HttpContext.User = claimsPrincipal;

                        // space separed value of user id, wallet id & email id 
                        string value = string.Format(@"{0} {1} {2}",
                                                        claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "jwt_userId")?.Value,
                                                        claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "jwt_walletId")?.Value,
                                                        claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "jwt_emailId")?.Value);

                        _redisService.Set(token, value, _expiryTime);
                        return;
                    }
                }
                catch (Exception)
                {
                    // Handle token validation errors (e.g., expired token, invalid signature)
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }

            // Unauthorized access if token is missing or invalid
            context.Result = new UnauthorizedResult();
        }

        #endregion
    }
}
