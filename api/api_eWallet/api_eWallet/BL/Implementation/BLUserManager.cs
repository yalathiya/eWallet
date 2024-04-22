using api_eWallet.BL.Interfaces;
using api_eWallet.Common;
using api_eWallet.Models.DTO;
using api_eWallet.Models.POCO;
using api_eWallet.Repository.Interfaces;
using api_eWallet.Services.Interfaces;
using System.Text.RegularExpressions;

namespace api_eWallet.BL.Implementation
{
    /// <summary>
    /// BL logic for user manager 
    /// </summary>
    public class BLUserManager : IBLUser
    {
        #region Private Members

        /// <summary>
        /// To use Credit Service 
        /// </summary>
        private IUserRepository _objUserRepo;

        /// <summary>
        /// POCO Model
        /// </summary>
        private Usr01 _objUsr01;

        /// <summary>
        /// Cryptography
        /// </summary>
        private ICryptography _cryptography;

        /// <summary>
        /// Sender Service
        /// </summary>
        private IEmailService _sender;

        #endregion

        #region Constructor 

        /// <summary>
        /// Reference of DI
        /// </summary>
        /// <param name="cryptography"></param>
        /// <param name="userRepository"></param>
        /// <param name="sender"></param>
        public BLUserManager(ICryptography cryptography, IUserRepository userRepository, IEmailService sender)
        {
            _cryptography = cryptography;
            _objUserRepo = userRepository;
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
        public bool Prevalidation(DTOUsr01 objDTOUsr01)
        {
            // check email id is available or not 
            if (!_objUserRepo.IsEmailIdAvailable(objDTOUsr01.r01101))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Convert DTO model to POCO Model 
        /// </summary>
        /// <param name="objDTOUsr01"> DTO of Cre01 </param>
        public void Presave(DTOUsr01 objDTOUsr01)
        {
            _objUsr01 = objDTOUsr01.ConvertModel<Usr01>();
        }

        /// <summary>
        /// Validate POCO Model 
        /// </summary>
        /// <returns>true if validated else false </returns>
        public bool Validate()
        {
            //validate email id
            if (!IsEmailValid(_objUsr01.r01f04))
            {
                return false;
            }

            // validate mobile number
            if (!IsMobileNumber(_objUsr01.r01f07))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Add or update user as per operation  
        /// </summary>
        /// <param name="opeartion"> Create => Create database record
        ///                          Update  => Update database record
        /// </param>
        public void Save(Operation op)
        {
            // Create Database Record
            if (Operation.Create == op)
            {
                // set encrypted password r01f03
                _objUsr01.r01f03 = _cryptography.Encrypt(_objUsr01.r01f03);

                // set creation time r01f08
                _objUsr01.r01f08 = DateTime.Now;

                // set updation time r01f09
                _objUsr01.r01f09 = DateTime.Now;

                // Create entry in database
                Dictionary<object, object> dictUserDetails =  _objUserRepo.RegisterUser(_objUsr01);

                // send user id and wallet id to user
                _sender.Send(_objUsr01.r01f04, $"Welcome to eWallet !! \r\n User Id : {dictUserDetails["r01f01"]} \r\n Wallet Id : {dictUserDetails["t01f01"]}");
    
            }
            // Update Database Record
            else if (Operation.Update == op)
            {
                // set updation time

                // update entry in database
            }
        }

        /// <summary>
        /// Get current user details
        /// </summary>
        /// <returns> DTO model of user </returns>
        public DTOUsr01 GetUserDetails(int id)
        {
            // fetch POCO model
            Usr01 objUsr01 = _objUserRepo.GetUserById(id);

            // convert to DTO model
            DTOUsr01 objDTOUsr01 = objUsr01.ConvertModel<DTOUsr01>();

            return objDTOUsr01;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks is Mobile number contsins 10 digits or not 
        /// </summary>
        /// <param name="r01f04"> Mobile Number </param>
        /// <returns> true for valid mobile number otherwise invalid </returns>
        private bool IsMobileNumber(string r01f04)
        {
            // Regex pattern to check length of digits 
            Regex mobileNumberPattern = new Regex(@"\d{10}");

            Match match = mobileNumberPattern.Match(r01f04);
            if (match.Success)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks email id is valid or not 
        /// </summary>
        /// <param name="email"> emailId </param>
        /// <returns> true => if emailId is valid
        ///           false => otherwise
        /// </returns>
        private bool IsEmailValid(string email)
        {
            // Regex pattern to validate email address
            Regex emailPattern = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            // Match the email against the pattern
            Match match = emailPattern.Match(email);

            // Return true if the email matches the pattern, otherwise false
            return match.Success;
        }

        #endregion
    }
}
