using api_eWallet.Models;
using api_eWallet.Models.DTO;
using api_eWallet.Utilities;

namespace api_eWallet.BL.Interfaces
{
    /// <summary>
    /// Interface for BLUserManager
    /// </summary>
    public interface IBLUsr01Handler
    {
        #region Public Members

        /// <summary>
        /// Type of operation 
        /// C => Create
        /// U => Update
        /// </summary>
        EnmOperation EnmOperation { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Prevalidates DTO model
        /// </summary>
        /// <param name="objDTOUsr01"> DTO model of user </param>
        /// <param name="userId"> user id extracted from claim </param>
        /// <returns> true => if validated successfully 
        ///           false => otherwise
        /// </returns>
        Response Prevalidation(DTOUsr01 objDTOUsr01, int userId);

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
        /// Add or update user as per EnmOperation  
        /// </summary>
        Response Save();

        /// <summary>
        /// Get Current User Details
        /// </summary>
        /// <param name="userId"> user id extracted from claim </param>
        /// <returns> DTO model of user </returns>
        Response GetUserDetails(int userId);

        #endregion
    }
}
