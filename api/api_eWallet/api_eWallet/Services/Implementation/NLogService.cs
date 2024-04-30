using api_eWallet.Services.Interfaces;
using NLog;

namespace api_eWallet.Services.Implementation
{
    /// <summary>
    /// Implementation of Ilogging Service 
    /// </summary>
    public class NLogService : ILogging
    {
        #region Private Members

        /// <summary>
        /// ILogger Service
        /// </summary>
        private readonly NLog.ILogger _logger;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public NLogService()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        #endregion

        #region Public Methods 

        /// <summary>
        /// Logs error
        /// </summary>
        /// <param name="message"> error message </param>
        public void LogError(string message)
        {
            _logger.Error(message);
        }

        /// <summary>
        /// Logs Exception
        /// </summary>
        /// <param name="ex"> exception </param>
        /// <param name="message"> exception message </param>
        public void LogException(Exception ex, string message = null)
        {
            _logger.Error(ex, message);
        }

        /// <summary>
        /// Logs information 
        /// </summary>
        /// <param name="message"> information message </param>
        public void LogInformation(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Logs trace 
        /// </summary>
        /// <param name="message"> trace message </param>
        public void LogTrace(string message)
        {
            _logger.Trace(message);
        }

        /// <summary>
        /// Logs Warning
        /// </summary>
        /// <param name="message"> warning message </param>
        public void LogWarning(string message)
        {
            _logger.Warn(message);
        }

        #endregion
    }
}
