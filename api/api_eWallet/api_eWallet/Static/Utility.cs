using api_eWallet.Services.Implementation;
using api_eWallet.Services.Interfaces;
using System.Reflection;
using System.Text.Json.Serialization;

namespace api_eWallet.Static
{
    /// <summary>
    /// Contains all necessary utilies within web api 
    /// </summary>
    public static class Utility
    {
        #region Static Methods

        /// <summary>
        /// extension methos which register all services in DI container 
        /// </summary>
        /// <param name="services"> collection of services </param>
        /// <returns> services </returns>
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
            // Register Aes Cryptography Service
            services.AddSingleton<ICryptography, AesCrptographyService>();
            
            // Register Email Service 
            services.AddSingleton<ISender, EmailService>();

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
                                                   .Where(prop => prop.IsDefined(typeof(JsonPropertyNameAttribute), false))
                                                   .ToArray();

            // Iterate through source properties
            foreach (PropertyInfo prop in sourceProps)
            {
                // Get the JSON property name from the attribute
                JsonPropertyNameAttribute attribute = (JsonPropertyNameAttribute)Attribute.GetCustomAttribute(prop, typeof(JsonPropertyNameAttribute));
                string targetPropName = attribute.Name;

                // Find corresponding property in target model
                PropertyInfo targetPropertyInfo = targetType.GetProperty(targetPropName);

                // Set the value of the target property from the source property
                targetPropertyInfo.SetValue(targetModel, prop.GetValue(sourceModel));
            }

            return targetModel;
        }

        #endregion

    }
}
