using api_eWallet.Models;

namespace api_eWallet.BL.Interfaces
{
    /// <summary>
    /// Interface for BL Settings 
    /// </summary>
    public interface IBLSettings
    {
        #region Public Methods

        /// <summary>
        /// Activate wallet 
        /// </summary>
        /// <param name="userId"> user id </param>
        /// <returns> object of response </returns>
        Response ActivateWallet(int userId);

        /// <summary>
        /// Deactivate wallet 
        /// </summary>
        /// <param name="userId"> user id </param>
        /// <returns> object of response </returns>
        Response DeactivateWallet(int userId);

        #endregion
    }
}
