using api_eWallet.Middlewares;
using api_eWallet.Middlewares.Filters;
using api_eWallet.Utilities;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;

namespace api_eWallet
{
    /// <summary>
    /// Class which consists configurations of application and services within the web api 
    /// </summary>
    public class Startup
    {
        #region Constructor

        /// <summary>
        /// Extract nlog configuration
        /// </summary>
        public Startup()
        {
            // providing nlog configuration file 
            NLog.LogManager.LoadConfiguration(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nlog.config"));
        }

        #endregion

        #region Public Methods 

        /// <summary>
        /// Configures all services 
        /// </summary>
        /// <param name="services"> refer to IServiceCollection interface (DI container of .NET) </param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Configures Controllers
            services.AddControllers(options =>
            {
                // Add JwtAuthenticationFilter as a global filter, excluding specific endpoint
                options.Filters.Add(typeof(JwtAuthenticationFilter));
            });


            // Configuring Logging
            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddNLog();
            });

            // Configuring Swagger 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eWallet", Version = "v1" });

                // Define the BearerAuth scheme that's in use
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // Define the requirement for BearerAuth
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Add Filters
            services.AddScoped<JwtAuthenticationFilter>();

            // Add all services in DI Container 
            services.AddMyServices();
        }

        /// <summary>
        /// Configure application
        /// </summary>
        /// <param name="app"> refer to IapllicationBuilder interface </param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();

            app.UseMiddleware<LoggingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }

        #endregion
    }
}
