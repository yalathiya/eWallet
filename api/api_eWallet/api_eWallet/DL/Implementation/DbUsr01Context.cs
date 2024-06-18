using api_eWallet.DL.Interfaces;
using api_eWallet.Utilities;
using System.Data;

namespace api_eWallet.DL.Implementation
{
    /// <summary>
    /// Implements IDbUsr01Context interface 
    /// </summary>
    public class DbUsr01Context : IDbUsr01Context
    {

        #region Public Methods

        /// <summary>
        /// Get user details by user id 
        /// </summary>
        /// <param name="r01f01"> user id </param>
        /// <returns> object of user details </returns>
        public object GetUsr01ById(int r01f01)
        {
            string query = String.Format(
                                                    @"SELECT 
                                                        R01f01 AS USER_ID,
                                                        R01f04 AS EMAIL_ID,
                                                        R01f05 AS FIRST_NAME,
                                                        R01f06 AS LAST_NAME,
                                                        R01f07 AS MOBILE_NUMBER
                                                     FROM
                                                        USR01 AS USER_DETAILS
                                                     WHERE
                                                        R01F01 = {0}",
                                                    r01f01);
            // Object of user details 
            DataTable userDetails = DbConnection.ExecuteQuery(query);

            return userDetails.ToList();
        }

        #endregion
    }
}
