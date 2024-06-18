using MySql.Data.MySqlClient;
using System.Data;

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

        public static DataTable ExecuteQuery(string query)
        {
            MySqlConnection _connection = new MySqlConnection(GetConnectionString());

            // Transaction details 
            DataTable dt = new DataTable();

            // retrieve user from the database in form of DataTable
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Connection = _connection;
                command.CommandText = query;

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dt);
            }

            return dt;
        } 

        #endregion
    }
}
