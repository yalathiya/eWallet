namespace api_eWallet.Services.Interfaces
{
    /// <summary>
    /// interface of Logging Service
    /// </summary>
    public interface ILogging
    {
        #region Public Methods

        /// <summary>
        /// Logs information 
        /// </summary>
        /// <param name="message"> information message </param>
        void LogInformation(string message);

        /// <summary>
        /// Logs Warning
        /// </summary>
        /// <param name="message"> warning message </param>
        void LogWarning(string message);

        /// <summary>
        /// Logs error
        /// </summary>
        /// <param name="message"> error message </param>
        void LogError(string message);

        /// <summary>
        /// Logs Exception
        /// </summary>
        /// <param name="ex"> exception </param>
        /// <param name="message"> exception message </param>
        void LogException(Exception ex, string message = null);

        /// <summary>
        /// Logs Trace message
        /// </summary>
        /// <param name="message"> message </param>
        void LogTrace(string message);

        /// <summary>
        /// Logs trace with user id 
        /// </summary>
        /// <param name="message"> message </param>
        /// <param name="userId"> user id ( folder name )</param>
        void LogTrace(string message, string userId);

        #endregion
    }
}
