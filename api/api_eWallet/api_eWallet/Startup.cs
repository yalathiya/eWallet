using api_eWallet.Filters;
using api_eWallet.Static;

namespace api_eWallet
{
    public class Startup
    {
        #region Public Members 

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();

            // Add Filters
            services.AddScoped<JwtAuthenticationFilter>();

            // Add all services in DI Container 
            services.AddMyServices();
            
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        #endregion
    }
}
