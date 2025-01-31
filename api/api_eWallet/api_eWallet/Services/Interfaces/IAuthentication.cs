﻿using System.Security.Claims;

namespace api_eWallet.Services.Interfaces
{
    /// <summary>
    /// Authentication interface 
    /// </summary>
    public interface IAuthentication
    {
        #region Public Methods

        /// <summary>
        /// Validate jwt token 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        ClaimsPrincipal ValidateToken(string token);

        /// <summary>
        /// Generate Jwt Token and also add roles
        /// </summary>
        /// <param name="emailId"> email id </param>
        /// <param name="userId"> user id </param>
        /// <param name="walletId"> wallet id </param>
        /// <returns> jwt token </returns>
        string GenerateJwtToken(string emailId, int userId, int walletId);

        #endregion

    }
}
