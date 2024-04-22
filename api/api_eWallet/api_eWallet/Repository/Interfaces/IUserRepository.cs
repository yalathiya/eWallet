using api_eWallet.Models.POCO;

namespace api_eWallet.Repository.Interfaces
{
    /// <summary>
    /// Interface of User Repository ( USR01 Table )
    /// </summary>
    public interface IUserRepository
    {
        #region Public Methods

        /// <summary>
        /// Add user in database
        /// </summary>
        /// <param name="objUsr01"> POCO model of user </param>
        /// <returns> details of registered user </returns>
        Dictionary<object, object> RegisterUser(Usr01 objUsr01);

        /// <summary>
        /// Is Credential Correct or not
        /// </summary>
        /// <param name="email"></param>
        /// <param name="encryptedPassword"></param>
        /// <returns></returns>
        bool IsCredentialCorrect(string email, string encryptedPassword);
        
        /// <summary>
        /// Is Email Id available or not 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsEmailIdAvailable(string email);
        
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <returns> POCO model of user </returns>
        Usr01 GetUserById(int id);

        #endregion

    }
}
