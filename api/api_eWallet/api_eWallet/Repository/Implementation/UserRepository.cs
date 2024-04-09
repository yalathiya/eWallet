using api_eWallet.Models.POCO;
using api_eWallet.Repository.Interfaces;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace api_eWallet.Repository.Implementation
{
    /// <summary>
    /// User repository - deals with table usr01 in database
    /// </summary>
    public class UserRepository : IUserRepository
    {
        #region Private Members

        private readonly IDbConnectionFactory _dbFactory;

        #endregion

        #region Public Members

        /// <summary>
        /// Reference of dbFactory
        /// </summary>
        /// <param name="dbFactory"> OrmLite Connection </param>
        public UserRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        /// <summary>
        /// Adds user in database 
        /// </summary>
        /// <param name="objUsr01"></param>
        public void RegisterUser(Usr01 objUsr01)
        {
            using (var db = _dbFactory.Open())
            {
                db.Insert(objUsr01);
            }
        }

        /// <summary>
        /// Is user credential correct or not
        /// </summary>
        /// <param name="email"> emailId </param>
        /// <param name="encryptedPassword"> password </param>
        /// <returns> true or false </returns>
        public bool IsCredentialCorrect(string email, string encryptedPassword)
        {
            using (var db = _dbFactory.Open())
            {
                // Check if there exists a user with the provided email and encrypted password
                return db.Exists<Usr01>(user => user.r01f04 == email && user.r01f03 == encryptedPassword);
            }
        }
    }
}
