using DomainLayer.Exceptions;
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
                await HandleNotFoundEndPointAsync(httpContext);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "something went wrong");
                await HandleException(httpContext, ex);
            }
        }

        private static async Task HandleException(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = new ErrorToReturn()
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };

            await httpContext.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"The end point{httpContext.Request.Path} is not found"
                };

                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
