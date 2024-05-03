using api_eWallet.Middlewares;
using api_eWallet.Middlewares.Filters;
using api_eWallet.Services.Implementation;
using api_eWallet.Utilities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using ServiceStack.Text;

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
            services.AddScoped<IsAccountActiveFilter>();

            // Add all services in DI Container 
            services.AddMyServices();

            // hosted service
            services.AddHostedService<BGNotificationDbClearService>();
        }

        /// <summary>
        /// Configure application
        /// </summary>
        /// <param name="app"> refer to IapllicationBuilder interface </param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // developer exception page in development environment
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // Added developer exception page
                // consists original line of code 
                app.UseDeveloperExceptionPage();
            }

            // exception handler in production environment 
            if (env.IsProduction())
            {
                // Added ExceptionHandler Page 
                // Content is delivered to web browser
                // It is used in production level, where original lines of code should not be visible to users.
                app.UseExceptionHandler((options) =>
                {
                    options.Run(async (context) =>
                    {
                        // configuring status code 
                        context.Response.StatusCode = 500;

                        // configuring response type as html
                        context.Response.ContentType = "text/html";

                        // get features from IExceptionHandlerFeature interface 
                        var ex = context.Features.Get<IExceptionHandlerFeature>();
                        if (ex != null)
                        {
                            // developing error 
                            string err = "<h1>This is custom error from Exception Handler</h1>" + // custom message 
                                          "<h2>Error Message :" + ex.Error.Message + "</h2>" + // error message
                                          "<h5>Error Stack" + ex.Error.StackTrace + "</h5>"; // stacktrace of error

                            // providing error to response 
                            await context.Response.WriteAsync(err);
                        }
                    });
                });
            }
            
            app.UseRouting();

            // Middlewares

            app.UseMiddleware<LoggingMiddleware>();
            app.UseMiddleware<RateLimitingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }

        #endregion
    }
}
