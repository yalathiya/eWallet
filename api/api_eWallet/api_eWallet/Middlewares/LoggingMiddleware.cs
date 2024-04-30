using api_eWallet.Services.Interfaces;
using System.Diagnostics;
using System.Text;

namespace api_eWallet.Middlewares
{
    /// <summary>
    /// Logging Middleware which logs request and response 
    /// </summary>
    public class LoggingMiddleware
    {
        #region Private Members

        /// <summary>
        /// refer to request delegate 
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Logging Support
        /// </summary>
        private readonly ILogging _logging;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor Injection
        /// </summary>
        /// <param name="next"> delegate </param>
        /// <param name="logging"> logging support </param>
        public LoggingMiddleware(RequestDelegate next, ILogging logging)
        {
            _next = next;
            _logging = logging;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Logs request & response
        /// </summary>
        /// <param name="context"> http context </param>
        /// <returns> logs information </returns>
        public async Task Invoke(HttpContext context)
        {
            // Log the request information
            LogRequest(context);

            // Start measuring the request processing time
            var stopwatch = Stopwatch.StartNew();

            // Call the next middleware in the pipeline
            await _next(context);

            // Stop measuring the request processing time
            stopwatch.Stop();

            // Log the response information
            LogResponse(context, stopwatch.ElapsedMilliseconds);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Logs request 
        /// </summary>
        /// <param name="context"> http context </param>
        private void LogRequest(HttpContext context)
        {
            var request = context.Request;

            // Log request method and path
            _logging.LogInformation($"Request: {request.Method} {request.Path}");

            // Log request headers
            LogHeaders("Request Headers:", request.Headers);

            // Log request body (if present)
            LogRequestBody(request);
        }

        /// <summary>
        /// Logs response 
        /// </summary>
        /// <param name="context"> http context </param>
        /// <param name="elapsedMilliseconds"> time taken for request processing </param>
        private void LogResponse(HttpContext context, long elapsedMilliseconds)
        {
            var response = context.Response;

            // Log response status code and elapsed time
            _logging.LogInformation($"Response: {response.StatusCode} (Elapsed Time: {elapsedMilliseconds} ms)");

            // Log response headers
            LogHeaders("Response Headers:", response.Headers);
        }

        /// <summary>
        /// Log headers
        /// </summary>
        /// <param name="title"> request header or response header </param>
        /// <param name="headers"> header data </param>
        private void LogHeaders(string title, IHeaderDictionary headers)
        {
            var builder = new StringBuilder();
            builder.AppendLine(title);
            foreach (var header in headers)
            {
                builder.AppendLine($"{header.Key}: {header.Value}");
            }
            _logging.LogInformation(builder.ToString());
        }

        /// <summary>
        /// Logs Request Body 
        /// </summary>
        /// <param name="request"> http request </param>
        private async void LogRequestBody(HttpRequest request)
        {
            // Ensure the request body is seekable and readable
            if (request.Body.CanSeek && request.Body.CanRead)
            {
                // Store the current position of the request body stream
                var position = request.Body.Position;

                // Read the request body stream and log its content
                request.Body.Seek(0, SeekOrigin.Begin);
                var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
                _logging.LogInformation($"Request Body: {requestBody}");

                // Restore the position of the request body stream
                request.Body.Seek(position, SeekOrigin.Begin);
            }
        }

        #endregion
    }
}