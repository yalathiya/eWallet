namespace api_eWallet.Services.Interfaces
{
    /// <summary>
    /// Interface for incryption & decryption of the data
    /// </summary>
    public interface ICryptography
    {
        #region Abstract Methods

        /// <summary>
        /// To encrypt data
        /// </summary>
        /// <param name="plainText"> plain text </param>
        /// <returns> cipher text </returns>
        string Encrypt(string plainText);

        /// <summary>
        /// To decrypt data
        /// </summary>
        /// <param name="cipherText"> cipher text </param>
        /// <returns> plain text </returns>
        string Decrypt(string cipherText);

        #endregion

    }
}
