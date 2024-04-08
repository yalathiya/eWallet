﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_eWallet.Controllers
{
    /// <summary>
    /// Controller for additional things in api
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLAdditional : ControllerBase
    {
        /// <summary>
        /// Redirects to api documentation for help
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("help")]
        public IActionResult Help()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// To change settings of the user within application
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        [Route("settings")]
        public IActionResult Settings()
        {
            throw new NotImplementedException();
        }

    }
}
