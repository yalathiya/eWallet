using api_eWallet.BL.Interfaces;
using api_eWallet.Models;
using api_eWallet.Models.POCO;
using api_eWallet.Utilities;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace api_eWallet.BL.Implementation
{
    /// <summary>
    /// Implementation of IBLSettings interface 
    /// </summary>
    public class BLSettingHandler : IBLSettingHandler
    {
        #region Private Members 

        /// <summary>
        /// Ormlite database factory
        /// </summary>
        private IDbConnectionFactory _dbFactory;

        /// <summary>
        /// object of response 
        /// </summary>
        private Response _objResponse;

        #endregion

        #region Constructor

        /// <summary>
        /// Dependency injection over constructor 
        /// </summary>
        /// <param name="dbFactory"></param>
        public BLSettingHandler(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
            _objResponse = new Response();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Activate wallet of user 
        /// </summary>
        /// <param name="userId"> user id </param>
        /// <returns> object of response </returns>
        public Response ActivateWallet(int userId)
        {
            using(var db = _dbFactory.Open())
            {
                db.Delete<Dac02>(acc => acc.C02f01 == userId);
            }

            _objResponse.SetResponse("Wallet is activated successfully", null);
            return _objResponse;
        }

        /// <summary>
        /// DeActivate wallet of user 
        /// </summary>
        /// <param name="userId"> user id </param>
        /// <returns> object of response </returns>
        public Response DeactivateWallet(int userId)
        {
            Dac02 objDac02 = new Dac02
            {
                C02f01 = userId
            };

            using (var db = _dbFactory.Open())
            {
                db.Insert<Dac02>(objDac02);
            }

            _objResponse.SetResponse("Wallet is Deactivated successfully", null);
            return _objResponse;
        }

        #endregion
    }
}
