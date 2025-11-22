using ECommerce.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Attributes
{
    public class CacheAttribute: ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Create Cache Key
            string CacheKey = GenerateCacheKey(context.HttpContext.Request);
            
            
            // Search in Cache 
            ICacheServices cacheServices = context.HttpContext.RequestServices.GetRequiredService<ICacheServices>();
            
            
            // return Value from Cache if not null
            var CacheValue = await cacheServices.GetAsync(CacheKey);


            // return Value from Cache if null
            if (CacheValue is not null)
            {
                // Return from Cache
                context.Result = new ContentResult()
                {
                    Content = CacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }


            // Invoke Next
            var ExecutedContext = await next.Invoke();
            
            
            // set Value in Cache Key
            if (ExecutedContext.Result is ObjectResult result)
            {
                await cacheServices.SetAsync(CacheKey, result.Value, TimeSpan.FromMinutes(2));
            }

        }

        private string GenerateCacheKey(HttpRequest request)
        {
            StringBuilder Key = new StringBuilder();
            Key.Append(request.Path + '?');
            foreach (var item in request.Query.OrderBy(x => x.Key))
            {
                Key.Append($"{item.Key}={item.Value}&");
            }
            return Key.ToString();
        }
    }
}
