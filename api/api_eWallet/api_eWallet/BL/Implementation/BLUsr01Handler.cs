using api_eWallet.BL.Interfaces;
using api_eWallet.DL.Interfaces;
using api_eWallet.Models;
using api_eWallet.Models.DTO;
using api_eWallet.Models.POCO;
using api_eWallet.Services.Interfaces;
using api_eWallet.Utilities;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Net;

namespace api_eWallet.BL.Implementation
{
    /// <summary>
    /// BL logic for user manager 
    /// </summary>
    public class BLUsr01Handler : IBLUsr01Handler
    {
        #region Public Members

        /// <summary>
        /// Type of operation 
        /// C => Create
        /// U => Update
        /// </summary>
        public EnmOperation EnmOperation { get; set; }

        #endregion

        #region Private Members

        /// <summary>
        /// POCO Model
        /// </summary>
        private Usr01 _objUsr01;

        /// <summary>
        /// Db Context of Usr01 
        /// </summary>
        private IDbUsr01Context _dbUsr01Context;

        /// <summary>
        /// OrmLite Connection Factory
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Cryptography
        /// </summary>
        private ICryptography _cryptography;

        /// <summary>
        /// Sender Service
        /// </summary>
        private IEmailService _sender;

        /// <summary>
        /// Response of Action Method
        /// </summary>
        private Response _objResponse;

        /// <summary>
        /// Logging Support 
        /// </summary>
        private ILogging _logging;

        #endregion

        #region Constructor 

        /// <summary>
        /// Reference of DI
        /// </summary>
        /// <param name="cryptography"> Cryptography Algorithm</param>
        /// <param name="dbFactory"> OrmLite database factory </param>
        /// <param name="sender"> sender service </param>
        /// <param name="logging"> logging support </param>
        /// <param name="dbUsr01Context"> db context of Usr01 </param>
        public BLUsr01Handler(ICryptography cryptography, 
                             IDbConnectionFactory dbFactory, 
                             IEmailService sender,
                             IDbUsr01Context dbUsr01Context,
                             ILogging logging)
        {
            _dbFactory = dbFactory;
            _dbUsr01Context = dbUsr01Context;
            _cryptography = cryptography;
            _sender = sender;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Prevalidates DTO model
        /// </summary>
        /// <param name="objDTOUsr01"> DTO model of user </param>
        /// <returns> true => if validated successfully 
        ///           false => otherwise
        /// </returns>
        public Response Prevalidation(DTOUsr01 objDTOUsr01, int userId)
        {
            _objResponse = new Response();
            
            if(EnmOperation == EnmOperation.C)
            {
                // Invalid Access
                if(objDTOUsr01.R01f01 != userId)
                {
                    _objResponse.SetResponse(true, HttpStatusCode.Forbidden, "Request is forbidden due to invalid user id. User id must be zero.", null);
                    return _objResponse;
                }

                // user cant create profile with existed email id in database 
                if (IsEmailIdExists(objDTOUsr01.R01f04))
                {
                    _objResponse.SetResponse(true, HttpStatusCode.BadRequest, "EmailId already exists in database", null);
                    return _objResponse;
                }
            }

            if(EnmOperation == EnmOperation.U)
            {
                // Invalid Access
                if (objDTOUsr01.R01f01 != userId)
                {
                    _objResponse.SetResponse(true, HttpStatusCode.Forbidden, "Request is forbidden due to invalid user id. You have no accesss of given user id.", null);
                    return _objResponse;
                }
            }

            _objResponse.SetResponse("Prevalidation Successful");
            return _objResponse;
        }

        /// <summary>
        /// Convert DTO model to POCO Model 
        /// </summary>
        /// <param name="objDTOUsr01"> DTO of Cre01 </param>
        public void Presave(DTOUsr01 objDTOUsr01)
        {
            _objUsr01 = objDTOUsr01.ConvertModel<Usr01>();
            _objUsr01.R01f03 = _cryptography.Encrypt(_objUsr01.R01f03);

            if (EnmOperation == EnmOperation.C)
            {
                _objUsr01.R01f08 = DateTime.Now;
            }
            else if(EnmOperation == EnmOperation.U)
            {
                _objUsr01.R01f09 = DateTime.Now;
            }
        }

        /// <summary>
        /// Validate POCO Model 
        /// </summary>
        /// <returns>true if validated else false </returns>
        public Response Validate()
        {
            _objResponse = new Response();

            if(EnmOperation == EnmOperation.C)
            {
                if (IsMobileExist(_objUsr01.R01f07))
                {
                    _objResponse.SetResponse(true, System.Net.HttpStatusCode.BadRequest, "Mobile Number already exists in database", null);
                    return _objResponse;
                }
            }
            else if(EnmOperation == EnmOperation.U)
            {
                if(IsEmailIdExists(_objUsr01.R01f04, _objUsr01.R01f01))
                {
                    _objResponse.SetResponse(true, System.Net.HttpStatusCode.BadRequest, "Email Id already exists in database", null);
                    return _objResponse;
                }

                if (IsMobileExist(_objUsr01.R01f07, _objUsr01.R01f01))
                {
                    _objResponse.SetResponse(true, System.Net.HttpStatusCode.BadRequest, "Mobile Number already exists in database", null);
                    return _objResponse;
                }
            }

            _objResponse.SetResponse("validated");
            return _objResponse;
        }

        /// <summary>
        /// save user entry in database 
        /// </summary>
        /// <returns> object of response s</returns>
        public Response Save()
        {
            _objResponse = new Response();

            // Create Database Record
            if (EnmOperation == EnmOperation.C)
            {
                int userId;
                int walletId;

                using (var db = _dbFactory.Open())
                {
                    userId = (int)db.Insert(_objUsr01, selectIdentity: true);

                    // Add Wallet Details 
                    Wlt01 objWlt01 = new Wlt01();
                    objWlt01.T01f02 = userId;
                    objWlt01.T01f03 = 0.0;
                    objWlt01.T01f04 = EnmCurrency.INR.ToString();
                    objWlt01.T01f05 = DateTime.Now;

                    walletId = (int)db.Insert(objWlt01, selectIdentity: true);
                }

                // send user id and wallet id to user
                _sender.Send(_objUsr01.R01f04, $"Welcome to eWallet !! \r\n User Id : {userId} \r\n Wallet Id : {walletId}");

                _logging.LogTrace("user is registered with user id : " + userId);
                _objResponse.SetResponse("User registered successfully");
                return _objResponse;    
            }
            // Update Database Record
            else if (EnmOperation == EnmOperation.U)
            {
                using(var db = _dbFactory.Open())
                {
                    db.Update<Usr01>(_objUsr01);
                }

                // send message to user 
                _sender.Send(_objUsr01.R01f04, $"Your user profile is updated successfully");

                _logging.LogTrace("user is updated with user id : " + _objUsr01.R01f01);
                _objResponse.SetResponse("User updated successfully");
                return _objResponse;
            }

            _objResponse.SetResponse(true, HttpStatusCode.InternalServerError, "error from server side", null);
            return _objResponse;
        }

        /// <summary>
        /// Get current user details
        /// </summary>
        /// <returns> DTO model of user </returns>
        public Response GetUserDetails(int userId)
        {
            _objResponse = new Response();

            _objResponse.SetResponse("Fetched user details", _dbUsr01Context.GetUsr01ById(userId));
            return _objResponse;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Is Email Id exists in database or not 
        /// </summary>
        /// <param name="email"> email id </param>
        /// <returns> true => email id exists in dtabase 
        ///           false => email id does not exist in database 
        /// </returns>
        public bool IsEmailIdExists(string email)
        {
            using (var db = _dbFactory.Open())
            {
                return db.Exists<Usr01>(user => user.R01f04 == email);
            }
        }

        /// <summary>
        /// Is email id exists in database or not except current record of user               
        /// </summary>
        /// <param name="r01f04"> email id </param>
        /// <param name="r01f01"> user id </param>
        /// <returns></returns>
        private bool IsEmailIdExists(string r01f04, int r01f01)
        {
            using (var db = _dbFactory.Open())
            {
                return db.Exists<Usr01>(usr => usr.R01f04 == r01f04 && usr.R01f01 != r01f01);
            }
        }


        /// <summary>
        /// Is mobile number exists in database or not         
        /// </summary>
        /// <param name="r01f07"> mobile number </param>
        /// <returns></returns>
        private bool IsMobileExist(string r01f07)
        {
            using (var db = _dbFactory.Open())
            {
                return db.Exists<Usr01>(usr => usr.R01f07 == r01f07);
            }
        }

        /// <summary>
        /// Is mobile number exists in database or not except current record of user               
        /// </summary>
        /// <param name="r01f07"> mobile number </param>
        /// <param name="r01f01"> user id </param>
        /// <returns></returns>
        private bool IsMobileExist(string r01f07, int r01f01)
        {
            using (var db = _dbFactory.Open())
            {
                return db.Exists<Usr01>(usr => usr.R01f07 == r01f07 && usr.R01f01 != r01f01);
            }
        }

        #endregion
    }
}
