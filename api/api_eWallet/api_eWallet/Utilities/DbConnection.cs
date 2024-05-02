namespace api_eWallet.Utilities
{
    /// <summary>
    /// Provides Database Connection
    /// </summary>
    public static class DbConnection
    {
        #region Private Members

        /// <summary>
        /// reference of configuration
        /// </summary>
        private static readonly IConfiguration _configuration;

        #endregion

        #region Constructors

        /// <summary>
        /// Collects data from appSettings.json file
        /// </summary>
        static DbConnection()
        {
            // It is compulsory to extract, its not option 
            // which shows second parameter
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        #endregion

        #region Public Methods

        ///// <summary>
        ///// Establishes the MySqlConnection
        ///// </summary>
        ///// <returns> MySql Connection </returns>
        //public static MySqlConnection CreateConnection()
        //{     
        //    return new MySqlConnection(GetConnectionString());
        //}

        /// <summary>
        /// To get connection string from appSettings.json
        /// </summary>
        /// <returns> connection string </returns>
        public static string GetConnectionString()
        {
            string server = _configuration["ConnectionStrings:connectionString:Server"];
            int port = int.Parse(_configuration["ConnectionStrings:connectionString:Port"]);
            string database = _configuration["ConnectionStrings:connectionString:Database"];
            string userId = _configuration["ConnectionStrings:connectionString:User Id"];
            string password = _configuration["ConnectionStrings:connectionString:Password"];

            // connection string 
            string connectionString = $"Server={server};Port={port};Database={database};User Id={userId};Password={password}";

            return connectionString;
        }

        #endregion
    }
}
