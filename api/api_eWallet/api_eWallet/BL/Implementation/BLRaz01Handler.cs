using api_eWallet.BL.Interfaces;
using api_eWallet.DL.Implementation;
using api_eWallet.DL.Interfaces;
using api_eWallet.Models;
using api_eWallet.Models.DTO;
using api_eWallet.Models.POCO;
using api_eWallet.Services.Interfaces;
using api_eWallet.Utilities;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Security.Cryptography;
using System.Text;

namespace api_eWallet.BL.Implementation
{
    /// <summary>
    /// Implemenatation of Raz01 interface
    /// Handles Razorpay Payments 
    /// </summary>
    public class BLRaz01Handler : IBLRaz01Handler
    {
        #region Private Members 

        /// <summary>
        /// POCO model of Raz01
        /// </summary>
        private Raz01 _objRaz01;

        /// <summary>
        /// Razorpay Service 
        /// </summary>
        private IRazorpayService _razorpayService;

        /// <summary>
        /// response to action method 
        /// </summary>
        private Response _objResponse;

        /// <summary>
        /// OrmLite db factory
        /// </summary>
        private IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Db context of transaction 
        /// </summary>
        private IDbTsn01Context _dbTsn01Context;

        /// <summary>
        /// configuration of app 
        /// </summary>
        private IConfiguration _config;

        #endregion

        #region Constructor

        /// <summary>
        /// Dependency injection over constructor
        /// </summary>
        /// <param name="razorpayService"> razorpay service </param>
        /// <param name="dbFactory"> ormlite db factory </param>
        /// <param name="dbTsn01Context"> db context of transaction </param>
        /// <param name="configuration"> app configuration </param>
        public BLRaz01Handler(IRazorpayService razorpayService,
                              IDbConnectionFactory dbFactory,
                              IDbTsn01Context dbTsn01Context,
                              IConfiguration configuration)
        {
            _config = configuration;
            _razorpayService = razorpayService;
            _dbFactory = dbFactory;
            _dbTsn01Context = dbTsn01Context;
        }

        #endregion

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
        /// Prevalidation of orderid 
        /// </summary>
        /// <param name="objDTORaz01"> dto model of Raz01 </param>
        /// <returns> object of response </returns>
        public Response Prevalidation(DTORaz01 objDTORaz01)
        {
            _objResponse = new Response();

            bool isOrderIdExists;
            using(var db = _dbFactory.Open())
            {
                isOrderIdExists = db.Exists<Raz01>(payment => payment.z01f01 == objDTORaz01.razorpay_order_id);
            }

            if (!isOrderIdExists)
            {
                _objResponse.SetResponse(true, System.Net.HttpStatusCode.Forbidden, "No order id exists in database", null);
                return _objResponse;
            }
            else
            {
                _objResponse.SetResponse("Prevalidation successful", null);
                return _objResponse;
            }
        }

        /// <summary>
        /// Convert DTO to POCO model
        /// </summary>
        /// <param name="objDTORaz01"> dto model of Raz01 </param>
        public void Presave(DTORaz01 objDTORaz01)
        {
            if(EnmOperation == EnmOperation.U)
            {
                _objRaz01 = new Raz01
                {
                    z01f01 = objDTORaz01.razorpay_order_id,
                    z01f02 = objDTORaz01.razorpay_payment_id,
                    z01f03 = objDTORaz01.razorpay_signature,
                    z01f07 = false,
                    z01f09 = DateTime.Now
                };
            }
        }

        /// <summary>
        /// add fields in POCO medel while order creation
        /// </summary>
        /// <param name="amount"> amount </param>
        /// <param name="walletId"> wallet id </param>
        public void Presave(double amount, int walletId)
        {
            if(EnmOperation == EnmOperation.C)
            {
                _objRaz01 = new Raz01 
                { 
                    z01f04 = amount/100,
                    z01f05 = walletId,
                    z01f08 = DateTime.Now,
                };
            }
        }

        /// <summary>
        /// Save record in database either create or update 
        /// </summary>
        /// <returns> object of response </returns>
        public Response Save()
        {
            _objResponse = new Response();

            // Create Order
            if (EnmOperation == EnmOperation.C)
            {
                Dictionary<string, string> requestData = new Dictionary<string, string>();
                requestData.Add("amount", Convert.ToString(_objRaz01.z01f04));
                requestData.Add("walletId", Convert.ToString(_objRaz01.z01f05));

                _objRaz01.z01f01 = _razorpayService.GenerateOrderId(requestData);

                using(var db = _dbFactory.Open())
                {
                    db.Insert<Raz01>(_objRaz01);
                }

                _objResponse.SetResponse("Order Created", _objRaz01.z01f01);
                return _objResponse;
            }
            // Process Payment
            else if (EnmOperation == EnmOperation.U)
            {
                // update in database - no reflection in wallet 
                using(var db = _dbFactory.Open())
                {
                    db.Update<Raz01>(_objRaz01);
                }

                // process transaction
                Tsn01 objTsn01 = new Tsn01
                {

                };
            }

            return _objResponse;
        }

        /// <summary>
        /// Validates POCO Model 
        /// </summary>
        /// <returns> object of response </returns>
        public Response Validate()
        {
            _objResponse = new Response();

            // Create Order
            if (EnmOperation == EnmOperation.C)
            {
                if (_objRaz01.z01f04 < 0)
                {
                    _objResponse.SetResponse(true, System.Net.HttpStatusCode.BadRequest, "Invalid Amount", null);
                    return _objResponse;
                }
            }
            // Process Payment
            else if (EnmOperation == EnmOperation.U)
            {
                // signature verification

                // Concatenate orderId and razorpayPaymentId with '|'
                string data = $"{_objRaz01.z01f01}|{_objRaz01.z01f02}";

                // Convert secret key to bytes
                byte[] secretBytes = Encoding.UTF8.GetBytes(_config["Razorpay:Secret"]);

                // Create HMAC-SHA256 instance with secret key
                using (HMACSHA256 hmac = new HMACSHA256(secretBytes))
                {
                    // Compute hash of the data
                    byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));

                    // Convert computed hash to string
                    string computedSignature = BitConverter.ToString(computedHash).Replace("-", "").ToLower();

                    // Compare computed signature with razorpaySignature
                    if (computedSignature == _objRaz01.z01f03)
                    {
                        _objResponse.SetResponse("Validation Successful", null);
                        return _objResponse;
                    }
                    else
                    {
                        _objResponse.SetResponse(true, System.Net.HttpStatusCode.BadRequest, "Signature validation failed", null);
                        return _objResponse;
                    }
                }
            }
            _objResponse.SetResponse(true, System.Net.HttpStatusCode.BadRequest, "Validation failed", null);
            return _objResponse;
        }

        #endregion
    }
}
