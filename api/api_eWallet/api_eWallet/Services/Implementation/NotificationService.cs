using api_eWallet.Models.POCO;
using api_eWallet.Services.Interfaces;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace api_eWallet.Services.Implementation
{
    /// <summary>
    /// Implementation of Notification service 
    /// </summary>
    public class NotificationService : INotificationService
    {
        #region Private Members 

        /// <summary>
        /// OrmLite DbFactory
        /// </summary>
        private IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Refer to email service
        /// </summary>
        private IEmailService _emailService;

        #endregion

        #region Constructor

        /// <summary>
        /// Dependency Injection over constructor
        /// </summary>
        /// <param name="dbFactory"> orm lite db factory </param>
        /// <param name="emailService"> email service </param>
        public NotificationService(IDbConnectionFactory dbFactory, 
                                   IEmailService emailService)
        {
            _dbFactory = dbFactory;
            _emailService = emailService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Send notification to user 
        /// </summary>
        /// <param name="objNot01"> object of notification </param>
        public void SendNotification(Not01 objNot01)
        {
            // adding into database 
            using(var db = _dbFactory.Open())
            {
                db.Insert<Not01>(objNot01);
            }

            //// by single field 
            // is email notification
            if (objNot01.T01f04)
            {
                // send email
                string email;
                using (var db = _dbFactory.Open())
                {
                    email = db.Select<Usr01>(user => user.R01f01 == objNot01.T01f02)
                                            .Select(user => user.R01f04)
                                            .FirstOrDefault();
                }
                _emailService.Send(email, objNot01.T01f03);
            }

        }

        #endregion
    }
}
