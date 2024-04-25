using api_eWallet.BL.Implementation;
using api_eWallet.BL.Interfaces;
using api_eWallet.DL.Implementation;
using api_eWallet.DL.Interfaces;
using api_eWallet.Models;
using api_eWallet.Services.Implementation;
using api_eWallet.Services.Interfaces;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Net;
using System.Reflection;
using System.Security.Claims;

namespace api_eWallet.Utilities
{
    /// <summary>
    /// Contains all necessary utilies within web api 
    /// </summary>
    public static class Utility
    {

        #region Public Methods

        /// <summary>
        /// extension methos which register all services in DI container 
        /// </summary>
        /// <param name="services"> collection of services </param>
        /// <returns> services </returns>
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
           
            // Register Aes Cryptography Service
            services.AddSingleton<ICryptography, AesCrptographyService>();

            // Connecting to OrmLite
            // Configure IDbConnectionFactory
            services.AddSingleton<IDbConnectionFactory>(c =>
                new OrmLiteConnectionFactory(
                    DbConnection.GetConnectionString(),
                    MySqlDialect.Provider));

            // Register Authentication Service 
            services.AddSingleton<IAuthentication, AuthenticationService>();

            // Register Email Service 
            services.AddSingleton<IEmailService, EmailService>();

            // Adding BLUserHandler
            services.AddScoped<IBLUsr01Handler, BLUsr01Handler>();

            // Adding BLAuthHandler
            services.AddScoped<IBLAuthHandler, BLAuthHandler>();

            // Adding BLWltHandler
            services.AddScoped<IBLWlt01Handler, BLWlt01Handler>();

            // Adding BLTsnHandler
            services.AddScoped<IBLTsn01Handler, BLTsn01Handler>();

            // Adding DbUsr01Context
            services.AddScoped<IDbUsr01Context, DbUsr01Context>();

            // Adding DbTsn01Context
            services.AddScoped<IDbTsn01Context, DbTsn01Context>();

            return services; 
        }

        /// <summary>
        /// Extension method that
        /// Converts properties of a source model to properties of a target model based on JSON property names.
        /// </summary>
        /// <typeparam name="T">The type of the target model.</typeparam>
        /// <param name="sourceModel">The source model whose properties are to be converted.</param>
        /// <returns>A new instance of the target model with properties populated from the source model.</returns>
        public static T ConvertModel<T>(this object sourceModel)
        {
            // generates blank instance of type T
            T targetModel = Activator.CreateInstance<T>();

            // Type contains className, all properties within class, etc..
            Type sourceType = sourceModel.GetType();
            Type targetType = typeof(T);

            // Get source properties with JSON property name attributes
            PropertyInfo[] sourceProps = sourceType.GetProperties().ToArray();

            // Iterate through source properties
            foreach (PropertyInfo sourceProp in sourceProps)
            {
                // get field name from source model
                string sourcePropName = sourceProp.Name;

                // fetch that field from target model
                PropertyInfo targetProp = targetType.GetProperty(sourcePropName);

                // If target model consists that field then assign value 
                targetProp?.SetValue(targetModel, sourceProp.GetValue(sourceModel, null), null);
            }

            return targetModel;
        }

        /// <summary>
        /// Extension method to get user id from jwt claims
        /// </summary>
        /// <param name="httpContext"> http context </param>
        /// <returns> user id </returns>
        public static int GetUserIdFromClaims(this HttpContext httpContext)
        {
            var principal = httpContext.User as ClaimsPrincipal;
            var userIdClaim = principal.FindFirst(c => c.Type == "jwt_userId");

            return Convert.ToInt32(userIdClaim.Value);
        }

        /// <summary>
        /// Extension method to get wallet id from jwt claims
        /// </summary>
        /// <param name="httpContext"> http context </param>
        /// <returns> wallet id </returns>
        public static int GetWalletIdFromClaims(this HttpContext httpContext)
        {
            var principal = httpContext.User as ClaimsPrincipal;
            var userIdClaim = principal.FindFirst(c => c.Type == "jwt_walletId");

            return Convert.ToInt32(userIdClaim.Value);
        }

        /// <summary>
        /// Extension method to get user id from jwt claims
        /// </summary>
        /// <param name="httpContext"> http context </param>
        /// <returns> email id </returns>
        public static string GetEmailIdFromClaims(this HttpContext httpContext)
        {
            var principal = httpContext.User as ClaimsPrincipal;
            var userIdClaim = principal.FindFirst(c => c.Type == "jwt_eamilId");
        
            return userIdClaim.Value;
        }


        /// <summary>
        /// Extension method that set Response message with all paramters 
        /// </summary>
        /// <param name="response"> response object</param>
        /// <param name="isErrorFlag"> error flag </param>
        /// <param name="statusCode"> status code </param>
        /// <param name="message"> message </param>
        /// <param name="data"> data </param>
        public static void SetResponse(this Response response, bool isErrorFlag, HttpStatusCode statusCode, string message, object data)
        {
            response.HasError = isErrorFlag;
            response.StatusCode = statusCode;
            response.Message = message;
            response.Data = data ;
        }

        /// <summary>
        /// Extension method thst set Response message which does not consist error 
        /// </summary>
        /// <param name="response"> response object</param>
        /// <param name="statusCode"> status code </param>
        /// <param name="message"> message </param>
        /// <param name="data"> data </param>
        public static void SetResponse(this Response response, HttpStatusCode statusCode, string message, object data)
        {
            response.StatusCode = statusCode;
            response.Message = message;
            response.Data = data;
        }

        /// <summary>
        /// Extension method that set response message with default status code 
        /// </summary>
        /// <param name="response"> response object </param>
        /// <param name="message"> message </param>
        /// <param name="data"> data </param>
        public static void SetResponse(this Response response, string message, object data)
        {
            response.Message = message;
            response.Data = data;
        }

        /// <summary>
        /// Extension method that set response message with default status code and no data 
        /// </summary>
        /// <param name="response"> response object </param>
        /// <param name="message"> message </param>
        public static void SetResponse(this Response response, string message)
        {
            response.Message = message;
        }

        #endregion

    }
}
