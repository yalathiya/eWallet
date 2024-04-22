using System.Net;

namespace api_eWallet.Models
{
    /// <summary>
    /// Response model of Action methods
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Errored response or not
        /// </summary>
        public bool HasError { get; set; } = false;

        /// <summary>
        /// Status code of the response
        /// </summary>
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK ;

        /// <summary>
        /// Response Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Response Data
        /// </summary>
        public object Data { get; set; }
    }
}
