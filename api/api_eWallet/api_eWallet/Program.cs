namespace api_eWallet
{
    /// <summary>
    /// Main class of the web api 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method of the application / api
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// adds configurations of startup class 
        /// </summary>
        /// <param name="args"></param>
        /// <returns> host builder </returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}