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
using System.Text.Json.Serialization;

namespace api_eWallet.Static
{
    /// <summary>
    /// Contains all necessary utilies within web api 
    /// </summary>
    public static class Utility
    {
        #region Extension Methods

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
                    Static.DbConnection.GetConnectionString(),
                    MySqlDialect.Provider));

            // Register Authentication Service 
            services.AddSingleton<IAuthentication, AuthenticationService>();

            // Register Email Service 
            services.AddSingleton<ISender, EmailService>();

            // Adding BLUser
            services.AddScoped<IBLUser, BLUserManager>();

            return services; // This allows chaining of registrations
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

        #endregion

        #region Static Methods

        /// <summary>
        /// Get userid from jwt token claim
        /// </summary>
        /// <param name="httpContextAccessor">The HttpContextAccessor instance to access HttpContext</param>
        /// <returns>userid (r01f01)</returns>
        /// <exception cref="InvalidOperationException">If issue related to find claim</exception>
        public static int GetUserIdFromClaims()
        {
            var httpContextAccessor = new HttpContextAccessor();
            var claimsIdentity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.Claims.FirstOrDefault(c => c.Type == "jwt_userId");

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            // Handle the case when the user ID is not found in claims or cannot be parsed
            throw new InvalidOperationException("User ID not found in claims or invalid.");

        }

        /// <summary>
        /// Get Wallet ID from jwt token claim
        /// </summary>
        /// <param name="httpContextAccessor">The HttpContextAccessor instance to access HttpContext</param>
        /// <returns>userid (r01f01)</returns>
        /// <exception cref="InvalidOperationException">If issue related to find claim</exception>
        public static int GetWalletIdFromClaims()
        {
            var httpContextAccessor = new HttpContextAccessor();

            var claimsIdentity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var walletIdClaim = claimsIdentity?.Claims.FirstOrDefault(c => c.Type == "jwt_walletId");

            if (walletIdClaim != null && int.TryParse(walletIdClaim.Value, out int walletId))
            {
                return walletId;
            }

            // Handle the case when the user ID is not found in claims or cannot be parsed
            throw new InvalidOperationException("User ID not found in claims or invalid.");

        }

        /// <summary>
        /// Get Email ID from jwt token claim
        /// </summary>
        /// <param name="httpContextAccessor">The HttpContextAccessor instance to access HttpContext</param>
        /// <returns>userid (r01f01)</returns>
        /// <exception cref="InvalidOperationException">If issue related to find claim</exception>
        public static string GetEmailIdFromClaims()
        {
            var httpContextAccessor = new HttpContextAccessor();

            var claimsIdentity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var emailIdClaim = claimsIdentity?.Claims.FirstOrDefault(c => c.Type == "jwt_emailId");

            if (emailIdClaim != null)
            {
                string emailId = emailIdClaim.Value;
                return emailId;
            }

            // Handle the case when the user ID is not found in claims or cannot be parsed
            throw new InvalidOperationException("User ID not found in claims or invalid.");

        }

        #endregion

    }
}
