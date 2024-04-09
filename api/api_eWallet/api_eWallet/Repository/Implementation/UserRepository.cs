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
                int r101f01 = (int)db.Insert(objUsr01, selectIdentity:true);

                // Add Wallet Details 
                Wlt01 objWlt01 = new Wlt01();
                objWlt01.t01f02 = r101f01;
                objWlt01.t01f05 = DateTime.Now;
                objWlt01.t01f06 = DateTime.Now;
                db.Insert(objWlt01);
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

        #endregion
    }
}
