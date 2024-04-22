using api_eWallet.Repository.Interfaces;
using api_eWallet.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace api_eWallet.Services.Implementation
{
    /// <summary>
    /// Implements IAuthentication Service
    /// </summary>
    public class AuthenticationService : IAuthentication
    {
        #region Private Members 
        
        /// <summary>
        /// Jwt Token Handler
        /// </summary>
        private readonly JwtSecurityTokenHandler _tokenHandler;

        /// <summary>
        /// Server configurations
        /// </summary>
        private readonly IConfiguration _configuration;
        
        /// <summary>
        /// Implements user repository
        /// </summary>
        private readonly IUserRepository _userRepository;
        
        /// <summary>
        /// Implements Cryptography
        /// </summary>
        private readonly ICryptography _cryptography;

        #endregion

        #region Constructor

        /// <summary>
        /// Take reference of DI from IoC
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="userRepository"></param>
        /// <param name="cryptography"></param>
        public AuthenticationService(IConfiguration configuration, 
                                     IUserRepository userRepository,
                                     ICryptography cryptography)
        {
            _configuration = configuration;
            _tokenHandler = new JwtSecurityTokenHandler();
            _userRepository = userRepository;
            _cryptography = cryptography;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Validates token 
        /// </summary>
        /// <param name="token"> jwt token </param>
        /// <returns> CLaims of jwt token </returns>
        /// <exception cref="Exception"></exception>
        public ClaimsPrincipal ValidateToken(string token)
        {
            try
            {
                var issuer = _configuration["JwtSettings:Issuer"];
                var audience = _configuration["JwtSettings:Audience"];
                var signingKey = _configuration["JwtSettings:SigningKey"];

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(signingKey))
                };

                ClaimsPrincipal claimsPrincipal = _tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                return claimsPrincipal;

            }
            catch (Exception ex)
            {
                // Handle token validation errors
                throw new Exception("Token validation failed.", ex);
            }
        }

        /// <summary>
        /// Generates Jwt Token 
        /// </summary>
        /// <param name="emailId"></param>
        /// <param name="userId"></param>
        /// <param name="walletId"></param>
        /// <returns> Jwt Token </returns>
        public string GenerateJwtToken(string emailId, int userId, int walletId)
        {
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var signingKey = _configuration["JwtSettings:SigningKey"];

            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(signingKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("jwt_emailId", emailId),
                new Claim("jwt_userId", Convert.ToString(userId)),
                new Claim("jwt_walletId", Convert.ToString(walletId)),
            };

            foreach (var role in roles)
            {
                claims.Append(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Token expires in 1 hour
                signingCredentials: credentials
            );

            return _tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Login Method 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string email, string password)
        {
            string encryptedPassword = _cryptography.Encrypt(password);

            if(_userRepository.IsCredentialCorrect(email, encryptedPassword))
            {
                // Login successful
                return true;
            }

            return false; // Login failed
        }

        #endregion
    }
}
