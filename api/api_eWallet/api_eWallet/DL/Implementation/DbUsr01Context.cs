using api_eWallet.DL.Interfaces;
using api_eWallet.Utilities;
using MySql.Data.MySqlClient;
using System.Data;

namespace api_eWallet.DL.Implementation
{
    /// <summary>
    /// Implements IDbUsr01Context interface 
    /// </summary>
    public class DbUsr01Context : IDbUsr01Context
    {
        #region Private Members

        /// <summary>
        /// MySql Connection 
        /// </summary>
        private MySqlConnection _connection;

        #endregion

        #region Constructor

        /// <summary>
        /// Configures MySql Connection
        /// </summary>
        public DbUsr01Context()
        {
            _connection = new MySqlConnection(DbConnection.GetConnectionString());
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get user details by user id 
        /// </summary>
        /// <param name="r01f01"> user id </param>
        /// <returns> object of user details </returns>
        public object GetUsr01ById(int r01f01)
        {
            // Create a DataTable
            DataTable dtUsr01 = new DataTable();

            // retrieve user from the database in form of DataTable
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Connection = _connection;
                command.CommandText = String.Format(@"SELECT 
                                                        R01f01 AS USER_ID,
                                                        R01f04 AS EMAIL_ID,
                                                        R01f05 AS FIRST_NAME,
                                                        R01f06 AS LAST_NAME,
                                                        R01f07 AS MONILE_NUMBER
                                                     FROM
                                                        USR01 AS USER_DETAILS
                                                     WHERE
                                                        R01F01 = {0}", r01f01);

                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    _connection.Open();
                    adapter.Fill(dtUsr01);
                }
                finally
                {
                    _connection.Close();
                }
            }
            return dtUsr01;
        }

        #endregion
    }
}
