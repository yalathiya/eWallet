using api_eWallet.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace api_eWallet.Services.Implementation
{
    /// <summary>
    /// Implements ICryptography interface using Aes Algorithm
    /// </summary>
    public class AesCrptographyService : ICryptography
    {
        #region Private Members 

        /// <summary>
        /// AES Crypto Service Provider class implements logic of AES Algorithm
        /// </summary>
        private AesCryptoServiceProvider _aes;
        
        /// <summary>
        /// Configuration of api 
        /// </summary>
        private IConfiguration _config;

        #endregion

        #region Constructor

        /// <summary>
        /// Provide Instance to class & extracts deta from configurations
        /// </summary>
        public AesCrptographyService(IConfiguration config)
        {
            _config = config;
            _aes = new AesCryptoServiceProvider();
            _aes.Key = Encoding.UTF8.GetBytes(_config["Cryptography:PrivateKey"]);
            _aes.IV = Encoding.UTF8.GetBytes(_config["Cryptography:InitialVector"]);
        }

        #endregion

        #region Public Members

        /// <summary>
        /// To encrypt data
        /// </summary>
        /// <param name="plainText"> plain text </param>
        /// <returns> cipher text </returns>
        public string Encrypt(string plainText)
        {
            // String to bytes
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            // Generate Key & Initilize Vector (IV)
            using (ICryptoTransform encryptor = _aes.CreateEncryptor())
            {
                // TransformFinalBlock method encrypts or decrypts data 
                // O is offset -> Means plaintext starting from begining
                // plainBytes.Length specifies the length of the message 
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                // Convert cipher text to string
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        /// <summary>
        /// To decrypt data
        /// </summary>
        /// <param name="cipherText"> cipher text </param>
        /// <returns> plain text </returns> 
        public string Decrypt(string cipherText)
        {
            // string to bytes
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            // Uses key & Initialize Vector (IV)
            using (ICryptoTransform decryptor = _aes.CreateDecryptor())
            {
                // TransformFinalBlock method encrypts or decrypts data from region of the data 
                // O is offset -> Means cipherText starting from begining
                // cipherBytes.Length specifies the length of the message 
                byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

                // Convert plain bytes to string
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        #endregion
    }
}
