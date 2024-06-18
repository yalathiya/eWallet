using api_eWallet.Models.POCO;
using api_eWallet.Services.Interfaces;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace api_eWallet.Services.Implementation
{
    /// <summary>
    /// Background service which clears old notification from the table
    /// </summary>
    public class BGNotificationDbClearService : BackgroundService
    {
        #region Private Members

        /// <summary>
        /// Logging Support 
        /// </summary>
        private readonly ILogging _logging;

        /// <summary>
        /// Ormlite database factory
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Dependency Injection over constructor 
        /// </summary>
        /// <param name="logging"> logging support </param>
        /// <param name="dbFactory"> db factory </param>
        public BGNotificationDbClearService(ILogging logging, 
                                            IDbConnectionFactory dbFactory)
        {
            _logging = logging;
            _dbFactory = dbFactory;
        }

        #endregion

        #region Protected Method

        /// <summary>
        /// Runs this service after every 24 hours
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Delete the old notification or perform cleanup operation
                _logging.LogWarning("Background service for clearing old notification from table started running .....");

                // Database logic
                DateTime oneDayAgo = DateTime.Now.AddDays(-1);

                using(var db = _dbFactory.Open())
                {
                    db.Delete<Not01>(x => x.T01f06 < oneDayAgo);
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // Run every 24 hours

                _logging.LogWarning("Cleared unnecessary notifications.....");
            }
        }

        #endregion
    }
}