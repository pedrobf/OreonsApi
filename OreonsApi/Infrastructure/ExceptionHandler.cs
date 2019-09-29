using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OreonsApi.core.customexceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OreonsApi.Infrastructure
{
    public class ExceptionHandler
    {
        #region Objects
        private readonly RequestDelegate _next;
        private ILogger _logger;
        #endregion

        #region Constructor
        public ExceptionHandler(RequestDelegate next)
        {
            this._next = next;
        }
        #endregion

        public async Task Invoke(HttpContext context, ILoggerFactory logger)
        {
            try
            {
                this._logger = logger.CreateLogger("ProductException");
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null) return;

            var code = HttpStatusCode.InternalServerError;

            if (exception is ArgumentException) code = HttpStatusCode.BadRequest;
            else if (exception is KeyNotFoundException) code = HttpStatusCode.NotFound;
            else if (exception is ProductConflictException) code = HttpStatusCode.Conflict;
            else if (exception is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;

            await WriteExceptionAsync(context, exception, code).ConfigureAwait(false);
        }

        private async Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        {
            if (code != HttpStatusCode.InternalServerError)
                this._logger.LogWarning(exception.Message);
            else
                this._logger.LogError(exception.Message);

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;
            await response.WriteAsync(JsonConvert.SerializeObject(exception.Message)).ConfigureAwait(false);
        }
    }
}
