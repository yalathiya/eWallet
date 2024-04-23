namespace api_eWallet.DL.Interfaces
{
    /// <summary>
    /// Interface for user database context
    /// </summary>
    public interface IDbUsr01Context
    {
        #region Public Methods

        /// <summary>
        /// Get user by user id 
        /// </summary>
        /// <param name="r01f01"> user id </param>
        /// <returns> user details </returns>
        object GetUsr01ById(int r01f01);

        #endregion
    }
}
