using api_eWallet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api_eWallet.Filters
{
    /// <summary>
    /// Authentication filter which valides jwt token
    /// </summary>
    public class JwtAuthenticationFilter : IAuthorizationFilter
    {
        #region Private Members

        private readonly IAuthentication _authService;

        #endregion

        #region Public Members

        /// <summary>
        /// Reference of IoC
        /// </summary>
        /// <param name="authService"> IAuthentication </param>
        public JwtAuthenticationFilter(IAuthentication authService)
        {
            _authService = authService;
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
            // Retrieve authorization header from request
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"];

            // Check if authorization header is present and has the Bearer scheme
            if (authorizationHeader.Count == 1 && authorizationHeader.FirstOrDefault().StartsWith("Bearer "))
            {
                var token = authorizationHeader.FirstOrDefault().Substring("Bearer ".Length);

                try
                {
                    // Validate the JWT token
                    var claimsPrincipal = _authService.ValidateToken(token);

                    if (claimsPrincipal != null)
                    {
                        // Set the User property on the context with the validated claims
                        context.HttpContext.User = claimsPrincipal;
                        return;
                    }
                }
                catch (Exception ex)
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
