using api_eWallet.Common;
using api_eWallet.Models;
using api_eWallet.Models.DTO;

namespace api_eWallet.BL.Interfaces
{
    /// <summary>
    /// Interface for BLUserManager
    /// </summary>
    public interface IBLUser
    {
        #region Public Methods

        /// <summary>
        /// Prevalidates DTO model
        /// </summary>
        /// <param name="objDTOUsr01"> DTO model of user </param>
        /// <returns> true => if validated successfully 
        ///           false => otherwise
        /// </returns>
        Response Prevalidation(DTOUsr01 objDTOUsr01);

        /// <summary>
        /// Convert DTO model to POCO Model 
        /// </summary>
        /// <param name="objDTOUsr01"> DTO of Cre01 </param>
        void Presave(DTOUsr01 objDTOUsr01);

        /// <summary>
        /// Validate POCO Model 
        /// </summary>
        /// <returns>true if validated else false </returns>
        Response Validate();

        /// <summary>
        /// Add or update user as per operation  
        /// </summary>
        /// <param name="opeartion"> Create => Create database record
        ///                          Update  => Update database record
        /// </param>
        Response Save(Operation op);
        
        /// <summary>
        /// Get Current User Details
        /// </summary>
        /// <returns> DTO model of user </returns>
        DTOUsr01 GetUserDetails(int userId);

        #endregion
    }
}
