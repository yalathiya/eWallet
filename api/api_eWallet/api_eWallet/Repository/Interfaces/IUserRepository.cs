using api_eWallet.Models.POCO;

namespace api_eWallet.Repository.Interfaces
{
    /// <summary>
    /// Interface of User Repository ( USR01 Table )
    /// </summary>
    public interface IUserRepository
    {
        #region Abstract Methods

        /// <summary>
        /// Add user details in database
        /// </summary>
        /// <param name="objDTOusr01"></param>
        void RegisterUser(Usr01 objUsr01);

        /// <summary>
        /// Is Credential Correct or not
        /// </summary>
        /// <param name="email"></param>
        /// <param name="encryptedPassword"></param>
        /// <returns></returns>
        bool IsCredentialCorrect(string email, string encryptedPassword);

        #endregion

    }
}
