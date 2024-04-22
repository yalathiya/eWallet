using api_eWallet.BL.Implementation;
using api_eWallet.BL.Interfaces;
using api_eWallet.Repository.Implementation;
using api_eWallet.Repository.Interfaces;
using api_eWallet.Services.Implementation;
using api_eWallet.Services.Interfaces;
using Newtonsoft.Json;
using ServiceStack.Data;
using ServiceStack.OrmLite;
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

            // Register User Repository Service 
            services.AddSingleton<IUserRepository, UserRepository>();

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

            // Adding BLUser
            services.AddScoped<IBLUser, BLUserManager>();

            return services; 
        }

        /// <summary>
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
            PropertyInfo[] sourceProps = sourceType.GetProperties()
                                                   .Where(prop => prop.IsDefined(typeof(JsonPropertyAttribute), false))
                                                   .ToArray();

            // Iterate through source properties
            foreach (PropertyInfo prop in sourceProps)
            {
                // Get the JSON property name from the attribute
                JsonPropertyAttribute attribute = (JsonPropertyAttribute)Attribute.GetCustomAttribute(prop, typeof(JsonPropertyAttribute));
                string targetPropName = attribute.PropertyName;

                // Find corresponding property in target model
                PropertyInfo targetPropertyInfo = targetType.GetProperty(targetPropName);

                // Set the value of the target property from the source property
                targetPropertyInfo.SetValue(targetModel, prop.GetValue(sourceModel));
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

        #endregion

    }
}
