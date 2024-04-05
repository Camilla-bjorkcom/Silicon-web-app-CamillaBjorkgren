using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web_API_Camilla.Filters;

[AttributeUsage(AttributeTargets.All)]
public class UseApiKeyAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = config?["ApiKey:Secret"];

        if (!string.IsNullOrEmpty(apiKey) && context.HttpContext.Request.Query.TryGetValue("key", out var key))
        {
            if(!string.IsNullOrEmpty(key) && apiKey == key)
            {
                await next();
                return;
            }          
        }
       
            context.Result = new UnauthorizedResult();       
    }
}
