using api_eWallet.Common;
using api_eWallet.Models.DTO;
using api_eWallet.Models.POCO;
using api_eWallet.Services.Interfaces;

namespace api_eWallet.BL.Interfaces
{
    /// <summary>
    /// Interface for BLUserManager
    /// </summary>
    public interface IBLUser
    {
        /// <summary>
        /// Convert DTO model to POCO Model 
        /// </summary>
        /// <param name="objDTOUsr01"> DTO of Cre01 </param>
        public void Presave(DTOUsr01 objDTOUsr01);

        /// <summary>
        /// Validate POCO Model 
        /// </summary>
        /// <returns>true if validated else false </returns>
        public bool Validate();

        /// <summary>
        /// Add or update user as per operation  
        /// </summary>
        /// <param name="opeartion"> Create => Create database record
        ///                          Update  => Update database record
        /// </param>
        public void Save(Operation op);
    }
}
