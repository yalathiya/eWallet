using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_eWallet.Controllers
{
    /// <summary>
    /// Controller for additional things in api
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLSettings : ControllerBase
    {
        #region Public Methods

        /// <summary>
        /// Redirects to api documentation for help
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        [AllowAnonymous]
        [Route("help")]
        public IActionResult Help()
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Help.pdf");
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/pdf", "Help.pdf");
        }

        /// <summary>
        /// To deactive users wallet 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("deactive")]
        public IActionResult DeactiveWallet()
        {
            
        }

        /// <summary>
        /// To active users wallet 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("active")]
        public IActionResult ActiveWallet()
        {

        }

        #endregion
    }
}
