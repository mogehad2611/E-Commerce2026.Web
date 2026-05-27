using Shared.ErrorModels;
using System.Text.Json;

namespace E_Commerce2026.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate requestDelegate;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate requestDelegate , ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            this.requestDelegate = requestDelegate;
            this._logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try 
            {
                await requestDelegate.Invoke(httpContext);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "something went wrong");

                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = ex.Message
                };

                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
