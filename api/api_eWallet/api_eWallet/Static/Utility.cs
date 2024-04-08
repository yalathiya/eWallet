using api_eWallet.Services.Implementation;
using api_eWallet.Services.Interfaces;

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

            return services; // This allows chaining of registrations
        }

        #endregion

    }
}
