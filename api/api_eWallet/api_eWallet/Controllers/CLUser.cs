﻿using api_eWallet.BL.Interfaces;
using api_eWallet.Models;
using api_eWallet.Models.Attributes;
using api_eWallet.Models.DTO;
using api_eWallet.Services.Interfaces;
using api_eWallet.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace api_eWallet.Controllers
{
    /// <summary>
    /// consists method which deals with user data
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLUser : ControllerBase
    {
        #region Private Members
        
        /// <summary>
        /// Implements IBLInterface 
        /// </summary>
        private readonly IBLUsr01Handler _objBLUserHandler;

        /// <summary>
        /// Response to Action Method
        /// </summary>
        private Response _objResponse;

        /// <summary>
        /// Logging support 
        /// </summary>
        private ILogging _logging;

        #endregion

        #region Constructor

        /// <summary>
        /// Reference from DI
        /// </summary>
        /// <param name="objBLUser"> bl layer of user </param>
        /// <param name="logging"> logging support </param>
        public CLUser(IBLUsr01Handler objBLUser, ILogging logging)
        {
            _objBLUserHandler = objBLUser;
            _logging = logging;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// For User Registration
        /// </summary>
        /// <param name="DTOUsr"> DTO Model of User </param>
        /// <returns> jwt token through login method if user register successfully
        ///           else BadRequest
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ATRateLimiting(MaxRequests = 1, TimeWindow = 2)]
        [Route("user")]
        public IActionResult Register([FromBody] DTOUsr01 objDTOUsr01)
        {
            _logging.LogTrace("***" + JsonConvert.SerializeObject(objDTOUsr01));

            _objBLUserHandler.EnmOperation = EnmOperation.C;

            // prevalidation
            _objResponse = _objBLUserHandler.Prevalidation(objDTOUsr01, 0);
            if (_objResponse.HasError)
            {
                return Ok(_objResponse);
            }

            // Presave
            _objBLUserHandler.Presave(objDTOUsr01);

            // validation
            _objResponse = _objBLUserHandler.Validate();
            if (_objResponse.HasError)
            {
                return Ok(_objResponse);
            }

            // save
            _objResponse = _objBLUserHandler.Save();

            return Ok(_objResponse);
        }

        /// <summary>
        /// Get user details
        /// </summary>
        /// <returns> User's Information </returns>
        [HttpGet]
        [ATRateLimiting(MaxRequests = 1, TimeWindow = 1)]
        [Route("info")]
        public IActionResult GetUserInfo()
        {
            return Ok(_objBLUserHandler.GetUserDetails(HttpContext.GetUserIdFromClaims()));
        }
    

        /// <summary>
        /// Update User Details 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        [ATRateLimiting(MaxRequests = 1, TimeWindow = 10)]
        public IActionResult UpdateUser([FromBody] DTOUsr01 objDTOUsr01)
        {
            _objBLUserHandler.EnmOperation = EnmOperation.U;

            // prevalidation
            _objResponse = _objBLUserHandler.Prevalidation(objDTOUsr01, HttpContext.GetUserIdFromClaims());
            if (_objResponse.HasError)
            {
                return Ok(_objResponse);
            }

            // Presave
            _objBLUserHandler.Presave(objDTOUsr01);

            // validation
            _objResponse = _objBLUserHandler.Validate();
            if (_objResponse.HasError)
            {
                return Ok(_objResponse);
            }

            // save
            _objResponse = _objBLUserHandler.Save();

            return Ok(_objResponse);
        }

        #endregion
    }
}
