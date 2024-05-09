using api_eWallet.Models;
using api_eWallet.Models.DTO;
using api_eWallet.Utilities;

namespace api_eWallet.BL.Interfaces
{
    /// <summary>
    /// Interface for Razorpay Payments 
    /// </summary>
    public interface IBLRaz01Handler
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
        /// Convert DTO model to POCO Model 
        /// </summary>
        /// <param name="objDTORaz01"> DTO model of Raz01 </param>
        void Presave(DTORaz01 objDTORaz01);
        
        /// <summary>
        /// Presaves amount and wallet id in POCO Model
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="walletId"></param>
        void Presave(double amount, int walletId);

        /// <summary>
        /// Validate POCO Model 
        /// </summary>
        /// <returns>object of response</returns>
        Response Validate();

        /// <summary>
        /// save razorpay payment as per operation   
        /// </summary>
        Response Save();

        #endregion
    }
}
