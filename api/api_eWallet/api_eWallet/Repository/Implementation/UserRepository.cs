using api_eWallet.Models.POCO;
using api_eWallet.Repository.Interfaces;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Legacy;

namespace api_eWallet.Repository.Implementation
{
    /// <summary>
    /// User repository - deals with table usr01 in database
    /// </summary>
    public class UserRepository : IUserRepository
    {
        #region Private Members
        
        /// <summary>
        /// OrmLite Connection Factory
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Reference of dbFactory
        /// </summary>
        /// <param name="dbFactory"> OrmLite Connection </param>
        public UserRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds user in database 
        /// </summary>
        /// <param name="objUsr01"></param>
        public Dictionary<object, object> RegisterUser(Usr01 objUsr01)
        {
            Dictionary<object, object> dictionaryUserDetails = new Dictionary<object, object>();

            using (var db = _dbFactory.Open())
            {
                int r01f01 = (int)db.Insert(objUsr01, selectIdentity:true);
                dictionaryUserDetails.Add("r01f01", r01f01);

                // Add Wallet Details 
                Wlt01 objWlt01 = new Wlt01();
                objWlt01.t01f02 = r01f01;
                objWlt01.t01f05 = DateTime.Now;
                objWlt01.t01f06 = DateTime.Now;
                
                int t01f01 = (int)db.Insert(objWlt01, selectIdentity:true);
                dictionaryUserDetails.Add("t01f01", t01f01);
            }

            return dictionaryUserDetails;
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

        /// <summary>
        /// Is Email Id available or not ?
        /// </summary>
        /// <param name="email"> email id </param>
        /// <returns> true => If user can use that email id
        ///           false => otherwise 
        /// </returns>
        public bool IsEmailIdAvailable(string email)
        {
            using (var db = _dbFactory.Open())
            {
                bool isPresentInDatabase = db.Exists<Usr01>(user => user.r01f04 == email);
                return !isPresentInDatabase;
            }
        }
        
        /// <summary>
        /// Get user by user Id
        /// </summary>
        /// <returns> POCO Model of user </returns>
        public Usr01 GetUserById(int id)
        {
            using (var db = _dbFactory.Open())
            {
                Usr01 objUsr01 = db.SingleById<Usr01>(id);

                return objUsr01;
            }
        }

        /// <summary>
        /// Get user id from email id
        /// </summary>
        /// <param name="email"> email id </param>
        /// <returns> user id </returns>
        public int GetUserId(string email)
        {
            using(var db = _dbFactory.Open())
            {
                int userId = db.Scalar<int>($"SELECT r01f01 FROM Usr01 WHERE r01f04 = {email}");
                return userId;
            }
        }

        #endregion
    }
}
