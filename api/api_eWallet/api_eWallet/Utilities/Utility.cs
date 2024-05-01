using api_eWallet.BL.Implementation;
using api_eWallet.BL.Interfaces;
using api_eWallet.DL.Implementation;
using api_eWallet.DL.Interfaces;
using api_eWallet.Models;
using api_eWallet.Models.POCO;
using api_eWallet.Services.Implementation;
using api_eWallet.Services.Interfaces;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;
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
            // register logging service
            services.AddSingleton<ILogging, NLogService>();

            // Register Aes Cryptography Service
            services.AddSingleton<ICryptography, AesCrptographyService>();

            // Connecting to OrmLite
            // Configure IDbConnectionFactory
            services.AddSingleton<IDbConnectionFactory>(c =>
                new OrmLiteConnectionFactory(
                    DbConnection.GetConnectionString(),
                    MySqlDialect.Provider));

            // Register RedisStackService
            services.AddSingleton<IRedisService, RedisService>();

            // Register Authentication Service 
            services.AddSingleton<IAuthentication, AuthenticationService>();
            
            // Register Email Service 
            services.AddSingleton<IEmailService, EmailService>();

            // Register Notification Service 
            services.AddSingleton<INotificationService, NotificationService>();

            // Adding BLUserHandler
            services.AddScoped<IBLUsr01Handler, BLUsr01Handler>();

            // Adding BLSettingsHandler
            services.AddScoped<IBLSettingHandler, BLSettingHandler>();

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

        /// <summary>
        /// Set Notification object 
        /// </summary>
        /// <param name="objNot01"> object of notification </param>
        /// <param name="userId"> user id </param>
        /// <param name="message"> notification message </param>
        /// <param name="isEmailNotification"> is email notification </param>
        /// <param name="isSmsNotication"> is sms notification </param>
        /// <param name="createdOn"> creation time </param>
        public static void SetNotification(this Not01 objNot01, int userId, string message, bool isEmailNotification, bool isSmsNotication, DateTime createdOn)
        {
            objNot01.T01f02 = userId;
            objNot01.T01f03 = message;
            objNot01.T01f04 = isEmailNotification;
            objNot01.T01f05 = isSmsNotication;
            objNot01.T01f06 = createdOn;
        }
        /// <summary>
        /// Extension method to convert DataTable to a list of objects
        /// </summary>
        /// <param name="dt"> data table </param>
        /// <returns> list of objects </returns>
        public static List<dynamic> ToList(this DataTable dt)
        {
            List<dynamic> list = new List<dynamic>();

            foreach (DataRow row in dt.Rows)
            {
                dynamic obj = new System.Dynamic.ExpandoObject();
                var dict = (IDictionary<string, object>)obj;

                // Populate the dynamic object with column values from the DataRow
                foreach (DataColumn column in dt.Columns)
                {
                    dict[column.ColumnName] = row[column];
                }

                list.Add(obj);
            }

            return list;
        }

        #endregion

    }
}
