using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attibutes
{
    internal class CacheAttribute(int DurationInSec=90) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string CacheKey = CreateCacheKey(request: context.HttpContext.Request);

            // Search For Value With cache Key
            ICacheService cacheService =
                context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cacheValue = await cacheService.GetAsync(CacheKey);

            // Return Value If Not Null
            if (cacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };

                return;
            }
            var ExecutedContext = await next.Invoke();

            // Set Value With Cache Key
            if (ExecutedContext.Result is OkObjectResult result)
            {
                await cacheService.SetAsync(
                     CacheKey,
                     result.Value,
                     TimeSpan.FromSeconds(value: DurationInSec)
                );
            }
        }

        private string CreateCacheKey(HttpRequest request)
        {
            // {{BaseUrl}}/api/Products?TypeId=20&BrandId=10
            StringBuilder Key = new StringBuilder();

            Key.Append(request.Path + '?');

            foreach (var Item in request.Query.OrderBy(Q => Q.Key))
            {
                Key.Append($"{Item.Key}={Item.Value}&");
            }

            return Key.ToString();
        }
    }
}
