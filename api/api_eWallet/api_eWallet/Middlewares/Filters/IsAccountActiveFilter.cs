using api_eWallet.Models.POCO;
using api_eWallet.Services.Interfaces;
using api_eWallet.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace api_eWallet.Middlewares.Filters
{
    /// <summary>
    /// Filter which checks account is deactivate or not 
    /// </summary>
    public class IsAccountActiveFilter : IActionFilter
    {
        #region Private Members

        /// <summary>
        /// Logging Support 
        /// </summary>
        private ILogging _logging;

        /// <summary>
        /// OrmLite Db Factory
        /// </summary>
        private IDbConnectionFactory _dbFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Dependency injection over constructor
        /// </summary>
        /// <param name="dbFactory"> ormlite db factory </param>
        /// <param name="logging"> logging support </param>
        public IsAccountActiveFilter(ILogging logging, 
                                     IDbConnectionFactory dbFactory)
        {
            _logging = logging;
            _dbFactory = dbFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// checks is user account is deactivated than it returns bad response 
        /// </summary>
        /// <param name="context"> context </param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            bool isDeactivated;

            using(var db = _dbFactory.Open())
            {
                isDeactivated = db.Exists<Dac02>(acc => acc.C02f01 == context.HttpContext.GetUserIdFromClaims());
            }

            if (isDeactivated)
            {
                _logging.LogWarning("Wallet is deactivated of user id " + context.HttpContext.GetUserIdFromClaims());
                context.Result = new BadRequestObjectResult("Wallet is deactivated");
                return;
            }

            _logging.LogTrace("Wallet is activated of user id " + context.HttpContext.GetUserIdFromClaims());
        }

        /// <summary>
        /// After filter execution it uses logging 
        /// </summary>
        /// <param name="context"> context </param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logging.LogTrace("Account validation filter is applied");
        }
        
        #endregion
    }
}
