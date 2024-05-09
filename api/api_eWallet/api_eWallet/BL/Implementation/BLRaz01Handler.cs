using api_eWallet.BL.Interfaces;
using api_eWallet.Models;
using api_eWallet.Models.DTO;
using api_eWallet.Utilities;

namespace api_eWallet.BL.Implementation
{
    /// <summary>
    /// Implemenatation of Raz01 interface
    /// Handles Razorpay Payments 
    /// </summary>
    public class BLRaz01Handler : IBLRaz01Handler
    {
        #region Public Members

        /// <summary>
        /// Type of operation 
        /// C => Create Order
        /// U => Update Order
        /// </summary>
        public EnmOperation EnmOperation { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Convert DTO to POCO model
        /// </summary>
        /// <param name="objDTORaz01"> dto model of Raz01 </param>
        public void Presave(DTORaz01 objDTORaz01)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// add fields in POCO medel while order creation
        /// </summary>
        /// <param name="amount"> amount </param>
        /// <param name="walletId"> wallet id </param>
        public void Presave(double amount, int walletId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save record in database either create or update 
        /// </summary>
        /// <returns> object of response </returns>
        public Response Save()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validates POCO Model 
        /// </summary>
        /// <returns> object of response </returns>
        public Response Validate()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
