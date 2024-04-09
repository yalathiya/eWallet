using api_eWallet.BL.Interfaces;
using api_eWallet.Common;
using api_eWallet.Models.DTO;
using api_eWallet.Models.POCO;
using api_eWallet.Repository.Implementation;
using api_eWallet.Repository.Interfaces;
using api_eWallet.Services.Interfaces;
using api_eWallet.Static;

namespace api_eWallet.BL.Implementation
{
    /// <summary>
    /// BL logic for user manager 
    /// </summary>
    public class BLUserManager : IBLUser
    {
        #region Private Members

        // To use Credit Service 
        private IUserRepository _objUserRepo;

        // POCO Moodel
        private Usr01 _objUsr01;

        // Cryptography
        private ICryptography _cryptography;

        #endregion

        #region Public Members 

        /// <summary>
        /// Reference of DI
        /// </summary>
        /// <param name="cryptography"></param>
        public BLUserManager(ICryptography cryptography, IUserRepository userRepository)
        {
            _cryptography = cryptography;
            _objUserRepo = userRepository;
        }

        /// <summary>
        /// Convert DTO model to POCO Model 
        /// </summary>
        /// <param name="objDTOUsr01"> DTO of Cre01 </param>
        public void Presave(DTOUsr01 objDTOUsr01)
        {
            _objUsr01 = objDTOUsr01.ConvertModel<Usr01>();
        }

        /// <summary>
        /// Validate POCO Model 
        /// </summary>
        /// <returns>true if validated else false </returns>
        public bool Validate()
        {
            return true;
        }

        /// <summary>
        /// Add or update user as per operation  
        /// </summary>
        /// <param name="opeartion"> Create => Create database record
        ///                          Update  => Update database record
        /// </param>
        public void Save(Operation op)
        {
            // Create Database Record
            if (Operation.Create == op)
            {
                // set encrypted password r01f03
                _objUsr01.r01f03 = _cryptography.Encrypt(_objUsr01.r01f03);

                // set creation time r01f08
                _objUsr01.r01f08 = DateTime.Now;

                // set updation time r01f09
                _objUsr01.r01f09 = DateTime.Now;

                // Create entry in database
                _objUserRepo.RegisterUser(_objUsr01);
            }
            // Update Database Record
            else if (Operation.Update == op)
            {
                // set updation time

                // update entry in database
            }
        }

        #endregion
    }
}
