using api_eWallet.BL.Interfaces;
using api_eWallet.Models;
using api_eWallet.Models.POCO;
using api_eWallet.Services.Interfaces;
using api_eWallet.Utilities;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Net;

namespace api_eWallet.BL.Implementation
{
    /// <summary>
    /// Implements interface of CLAuth handler
    /// </summary>
    public class BLAuthHandler : IBLAuthHandler
    {
        #region Private Members

        /// <summary>
        /// Implementation of cryptography
        /// </summary>
        private ICryptography _cryptography;

        /// <summary>
        /// Response of action method
        /// </summary>
        private Response _objResponse;

        /// <summary>
        /// OrmLite Connection Factory
        /// </summary>
        private IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Authentication Service 
        /// </summary>
        private IAuthentication _authService;

        #endregion

        #region Constructor

        /// <summary>
        /// Configures necessary dependency injections
        /// </summary>
        public BLAuthHandler(ICryptography cryptography, IDbConnectionFactory dbFactory, IAuthentication authentication)
        {
            _cryptography = cryptography;
            _dbFactory = dbFactory;
            _authService = authentication;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Login Method 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Response Login(string email, string password)
        {
            string encryptedPassword = _cryptography.Encrypt(password);

            _objResponse = new Response();

            int userId;
            int walletId;

            // Credential Verification & 
            // extracting information to be added in claims
            using(var db = _dbFactory.Open())
            {
                Usr01 objUsr01 = db.Single<Usr01>(usr => usr.R01f04 == email && usr.R01f03 == encryptedPassword);
                // Invalid Credential
                if(objUsr01 == null)
                {
                    _objResponse.SetResponse(true, HttpStatusCode.BadRequest, "Login Unsuccessful", null);
                    return _objResponse;
                }
                userId = objUsr01.R01f01;
                walletId = db.Single<Wlt01>(wlt => wlt.T01f02 == userId).T01f01;
            }

            string token = _authService.GenerateJwtToken(email, userId, walletId);

            _objResponse.SetResponse("Login Successful", new { token = token });
            return _objResponse;
        }

        #endregion
    }
}
